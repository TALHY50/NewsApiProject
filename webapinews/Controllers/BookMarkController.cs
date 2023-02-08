using Microsoft.AspNetCore.Mvc;
using webapinews.Entities;
using webapinews.Interface;
using webapinews.Models;
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
        [HttpGet]
        public IActionResult GetAllNews()
        {
            var users = _bookMark.GetAll();
            return Ok(users);
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
