using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapinews.Command.BookMark_Commands;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Qurey.BookMark_Qurey;
using webapinews.Qurey.News_Qurey;
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
        private readonly IMediator _mediator;
        private readonly IBookMarkReporistory _bookMark;
        private IIdentityService _identityService;

        public BookMarkController(IBookMarkReporistory bookMark,IIdentityService identityService, IMediator mediator) 
        
        {
            _bookMark = bookMark;
            _identityService= identityService;
            _mediator= mediator;
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpGet("[action]")]
        public async Task<ActionResult<List<News>>> GetAll()
        {
            if (_mediator == null)
            {
                return NotFound("Not Found");

            }
            return await _mediator.Send(new GetNewsListQuery());
        }
        [Authorize(Role.Admin, Role.User)]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<News>>> Get([FromQuery] OwnerStringParameter ownerStringParameter)
        {
            if (_mediator == null)
            {
                return NotFound("No Data found");
            }
            var newsList = await _mediator.Send(new GetNewsPagenatedQuery(ownerStringParameter));
            var metadata = new
            {
                newsList.TotalCount,
                newsList.PageSize,
                newsList.CurrentPage,
                newsList.TotalPages,
                newsList.HasNextPage,
                newsList.HasPreviousPage
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(metadata);
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
            var user = await _mediator.Send(new GetBookMarkByIdQuery(currentUser.Value));
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
            var model =  await _mediator.Send(new PagenatedBookMarkNewsQurey(currentUser.Value, ownerStringParameter));
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
            var result = _mediator.Send(new SaveBookMarkCommand(id, currentUser.Value));

            if (result == null)
            {
                return NotFound("Enter the valid email or News ID");
            }
            return Ok(new { message = "News successfully BookMark" });
        }

       
        [Authorize(Role.Admin, Role.User)]
        [HttpDelete("{Id}")]
        public async Task<ActionResult<BookMark>> Delete(int id)
        {
            var currentUser = _identityService.GetUserId();
            var result = _mediator.Send(new DeleteBookMarkCommand(id, currentUser.Value));
            if (result == null)
            {
                return NotFound("No News with such ID Found");
            }
            return Ok(new { message = "BookMarked News successfully Deleted" });
        }

    }
}
