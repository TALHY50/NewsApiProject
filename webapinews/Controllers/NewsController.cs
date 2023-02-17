using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapinews.Command.News_Commands;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Mappers.News_Mapper;
using webapinews.Mappers.NewsMapper;
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
        [HttpGet]
        public async Task<ActionResult<List<NewsViewModel>>> Get()
        {
            if (_mediator == null)
            {
                return NotFound("Not Found");

            }
            return Ok(await _mediator.Send(new GetNewsListQuery()));
        }
        [AllowAnonymous]
        [HttpGet("Paginated")]
        public async Task<ActionResult<PaginatedList<NewsViewModel>>> GetPaginated([FromQuery] PaginatedViewModel paginatedViewModel)
        {
            if (_mediator == null)
            {
                return NotFound("No Data found");
            }
            return Ok(await _mediator.Send(new GetNewsPagenatedQuery(paginatedViewModel)));
        }

        [Authorize(Role.Admin, Role.User)]
        [HttpGet("Id")]

        public async Task<ActionResult<NewsViewModel>> GetById(int id)
        {
            if (_mediator == null)
                return Unauthorized(new { message = "Unauthorized" });
       
            return Ok(await _mediator.Send(new GetNewsByIdQuery(id)));
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<AddNewsViewModel>> Post(AddNewsCommand model)
        {
            if (model == null)
            {
                return BadRequest(new { message = "Invalid Request" });
            }
            return Ok(await _mediator.Send(model));
        }
        [Authorize(Role.Admin)]
        [HttpPut]
        public async Task<ActionResult<UpdateNewsViewModel>> Update(int id, UpdateNewsCommand model)
        {
            if(model == null)
            {
                return NotFound(new { message = "News Id Not Found " });
            }
            model.Id = id;
                 
            return Ok(await _mediator.Send(model));
        }

        [Authorize(Role.Admin)]
        [HttpDelete]
        public async Task<ActionResult<string>> Delete(int id)
        {
            if(_mediator == null)
            {
                return BadRequest(new { message = "Request Deleted News Not Found" });
            }
            return Ok(await _mediator.Send(new DeleteNewsCommand(id)));

        }
    }
}
