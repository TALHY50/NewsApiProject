using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapinews.Command.BookMark_Commands;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Mappers.BookMarkMapper;
using webapinews.Mappers.News_Mapper;
using webapinews.Mappers.NewsMapper;
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
    public class BookMarkNewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private IIdentityService _identityService;

        public BookMarkNewsController(IIdentityService identityService, IMediator mediator)

        {

            _identityService = identityService;
            _mediator = mediator;
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpGet]
        public async Task<ActionResult<List<NewsViewModel>>> Get()
        {
            if (_mediator == null)
            {
                return NotFound("Not Found");

            }
            return await _mediator.Send(new GetNewsListQuery());
        }
        //[Authorize(Role.Admin, Role.User)]
        [AllowAnonymous]
        [HttpGet("Paginated")]
        public async Task<ActionResult<PaginatedList<News>>> GetPaginated([FromQuery] PaginatedViewModel paginatedViewModel)
        {
            if (_mediator == null)
            {
                return NotFound("No Data found");
            }
            var newsList = await _mediator.Send(new GetNewsPagenatedQuery(paginatedViewModel));


            return Ok(newsList);
        }

        [Authorize(Role.Admin , Role.User)]
        [HttpGet("ActiveUser")]
        public async Task<ActionResult<List<BookMarksViewModel>>> GETActiveUser()
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
        [HttpGet("ActiveUser/Paginated")]
        public async Task<ActionResult<PaginatedList<BookMarksViewModel>>> GetPaginatedActiveUser([FromQuery] PaginatedViewModel paginatedViewModel)
        {
            var currentUser = _identityService.GetUserId();
            if (currentUser == null)
            {
                return Unauthorized(new { message = "Unauthorized" });
            }
            var model = await _mediator.Send(new PagenatedBookMarkNewsQurey(currentUser.Value, paginatedViewModel));
            return Ok(model);
        }


        [Authorize(Role.Admin, Role.User)]
        [HttpPost]
        public async Task<ActionResult<List<BookMark>>> Post(int id)
        {
            var currentUser = _identityService.GetUserId();
            var result = await _mediator.Send(new SaveBookMarkCommand(id, currentUser.Value));

            if (result == null)
            {
                return NotFound("Enter the valid email or News ID");
            }
            return Ok(new { message = "News successfully BookMark" });
        }


        [Authorize(Role.Admin, Role.User)]
        [HttpDelete]
        public async Task<ActionResult<string>> Delete(int id)
        {
            var currentUser = _identityService.GetUserId();
            if (currentUser == null)
            {
                return NotFound("No News with such ID Found");
            }
            return Ok(await _mediator.Send(new DeleteBookMarkCommand(id, currentUser.Value)));
        }

    }
}
