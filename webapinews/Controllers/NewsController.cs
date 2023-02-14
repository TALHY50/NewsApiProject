using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapinews.Command.News_Commands;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;
using AllowAnonymousAttribute = webapinews.Services.AllowAnonymousAttribute;
using AuthorizeAttribute = webapinews.Services.AuthorizeAttribute;

namespace webapinews.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<ActionResult<List<News>>> Get()
        {
            if (_mediator == null)
            {
                return NotFound("Not Found");

            }
            return await _mediator.Send(new GetNewsListQuery());
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<News>>> Get([FromQuery] OwnerStringParameter ownerStringParameter)
        {
            if (_mediator == null)
            {
                return NotFound("No Data found");
            }
            var newsTask = _mediator.Send(new GetNewsPagenatedQuery(ownerStringParameter));
            var newsList = await newsTask;
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
            return Ok(newsList);
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpGet("{id}")]

        public async Task<ActionResult<News>> GetById(int id)
        {
            if(_mediator == null)
            {
                return NotFound("No Record Found");
            }
            var result =  await _mediator.Send(new GetNewsByIdQuery(id));
            return result;
        }

        [Authorize(Role.Admin)]
        [HttpPost("[action]")]
        public async Task<ActionResult<News>> Post(News news)
        {
            if (news == null)
            {
                return BadRequest(new { message = "News Was Not SucessFully Added" });
            }
             await _mediator.Send(new AddNewsCommand(news));
            return Ok(new { message = "News updated successfully" });
        }

        [Authorize(Role.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult<News>> Update(int id, News news)
        {
            if(news == null)
            {
                return BadRequest(new { message = "News Id Not Found " });
            }
            _mediator?.Send(new UpdateNewsCommand(id, news));
            return Ok(new { message = "News updated successfully" });
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<News>> Delete(int id)
        {
            if(_mediator == null)
            {
                return BadRequest(new { message = "Request Deleted News Not Found" });
            }
            _mediator?.Send(new DeleteNewsCommand(id));
            return Ok(new { message = "News deleted successfully" });

        }
    }
}
