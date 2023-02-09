using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Reporistory;
using AuthorizeAttribute = webapinews.Services.AuthorizeAttribute;


namespace webapinews.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookMarkController : ControllerBase
    {
        private readonly IBookMark _bookMark;

        public BookMarkController(IBookMark bookMark) {
            _bookMark = bookMark;

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
            var currentUser = (User)HttpContext.Items["User"];
            if (currentUser == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }
            var user = _bookMark.GetById(currentUser.Id).Where(x => x.IsBookMark == true);
            return Ok(user);
        }
        [Authorize(Role.Admin, Role.User)]
        [HttpGet("[action]")]
        public async Task<ActionResult<PaginatedList<BookMarksViewModel>>> GetBookMarKByActiveId([FromQuery] OwnerStringParameter ownerStringParameter)
        {
            var currentUser = (User)HttpContext.Items["User"];
            if (currentUser == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }
            var model = _bookMark.GetBookMarkedById(currentUser.Id, ownerStringParameter);
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
            var currentUser = (User)HttpContext.Items["User"];
            var result = _bookMark.BookMarkNews(id, currentUser.Id);

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
            var currentUser = (User)HttpContext.Items["User"];
            var result = _bookMark.Delete(id, currentUser.Id);

            if (result == null)
            {
                return NotFound("No News with such ID Found");
            }
            return Ok(result);
        }

    }
}
