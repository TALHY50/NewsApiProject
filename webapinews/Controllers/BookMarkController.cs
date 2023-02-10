using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Reporistory;
using webapinews.Services;
using AuthorizeAttribute = webapinews.Services.AuthorizeAttribute;


namespace webapinews.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookMarkController : ControllerBase
    {
        private readonly IBookMarkReporistory _bookMark;
        private IIdentityService _identityService;

        public BookMarkController(IBookMarkReporistory bookMark,IIdentityService identityService) 
        
        {
            _bookMark = bookMark;
            _identityService= identityService;
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpGet("[action]")]
        public IActionResult GetAllNews()
        {
            var users = _bookMark.GetAll();
            return Ok(users);
        }
        [Authorize(Role.Admin, Role.User)]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<News>>> Get([FromQuery] OwnerStringParameter ownerStringParameter)
        {
            if (_bookMark == null)
            {
                return NotFound("No Data found");
            }
            var model = _bookMark.Get(ownerStringParameter);
            var metadata = new
            {
                model.TotalCount,
                model.PageSize,
                model.CurrentPage,
                model.TotalPages,
                model.HasNextPage,
                model.HasPreviousPage
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(model);
        }

        [Authorize(Role.Admin , Role.User)]
        [HttpGet("[action]")]
        public async Task<ActionResult<List<BookMarksViewModel>>> Get()
        {
            var currentUser = _identityService.GetUserId();
            if (currentUser == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }
            var user = _bookMark.GetById(currentUser.Value).Where(x => x.IsBookMark == true);
            return Ok(user);
        }
        [Authorize(Role.Admin, Role.User)]
        [HttpGet("[action]")]
        public async Task<ActionResult<PaginatedList<BookMarksViewModel>>> GetBookMarKByActiveId([FromQuery] OwnerStringParameter ownerStringParameter)
        {
            var currentUser = _identityService.GetUserId();
            if (currentUser == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }
            var model = _bookMark.GetBookMarkedById(currentUser.Value, ownerStringParameter);
            var metadata = new
            {
                model.Search,
                model.TotalCount,
                model.PageSize,
                model.CurrentPage,
                model.TotalPages,
                model.HasNextPage,
                model.HasPreviousPage
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(model);
        }


        [Authorize(Role.Admin, Role.User)]
        [HttpPost]
        public async Task<ActionResult<List<BookMark>>> SaveBookmark(int id)
        {
            var currentUser = _identityService.GetUserId();
            var result = _bookMark.BookMarkNews(id, currentUser.Value);

            if (result == null)
            {
                return NotFound("Enter the valid email or News ID");
            }
            return Ok(result);
        }

       
        [Authorize(Role.Admin, Role.User)]
        [HttpDelete("{Id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var currentUser = _identityService.GetUserId();
            var result = _bookMark.Delete(id, currentUser.Value);
            if (result == null)
            {
                return NotFound("No News with such ID Found");
            }
            return Ok(result);
        }

    }
}
