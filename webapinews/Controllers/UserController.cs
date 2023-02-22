using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapinews.Command.User_Commands;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Helpers.Paging;
using webapinews.Interface;
using webapinews.Mappers;
using webapinews.Mappers.UserMapper;
using webapinews.Models;
using webapinews.Qurey.News_Qurey;
using webapinews.Qurey.User_Qurey;
using AllowAnonymousAttribute = webapinews.Services.AllowAnonymousAttribute;
using AuthorizeAttribute = webapinews.Services.AuthorizeAttribute;

namespace webapinews.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IIdentityService _identityService;
        private readonly IMediator _mediator;

        public UserController(IMediator mediator, IIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<UserInputResponse>> Login(UserInputModel model)
        {
            if (model == null)
            {
                return NotFound(new { message = "NO User Found" });
            }
            var response = await _mediator.Send(new loginCommand(model));
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("Registor")]
        public async Task<ActionResult<RegistorUserViewModel>> Registor(RegistorUserCommand model)
        {
            if (model == null)
            {
                return Ok(new { message = "Invalid Credential" });
            }

            return Ok(await _mediator.Send(model));
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<UserViewModel>> Get()
        {
            var users = await _mediator.Send(new GetUserListQuery());
            return Ok(users);
        }
        [Authorize(Role.Admin)]
        [HttpGet("Paginated")]
        public async Task<ActionResult<PaginatedList<UserViewModel>>> GetPaginated([FromQuery] PaginatedViewModel paginatedViewModel)
        {
            if (_mediator == null)
            {
                return NotFound("No Data found");
            }

            return Ok(await _mediator.Send(new GetUserPagenatedQuery(paginatedViewModel)));

        }
        //[Authorize(Role.Admin)]
        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserViewModel>> GetById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }
        [Authorize(Role.Admin)]
        [HttpPut]
        public async Task<ActionResult<UpdateUserViewModel>> Update(int Id, UpdateUserCommand model)
        {
            if (model == null)
            {
                return Ok(new { message = "User Id not Found" });
            }
            model.Id = Id;

            return Ok(await _mediator.Send(model));
        }
        [Authorize(Role.Admin)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<UserViewModel>> Delete(int id) {
            var currentUser = _identityService.GetUserId();
            if (id != currentUser)
            return Unauthorized(new { message = "Unauthorized" });
            return Ok(await _mediator.Send(new DeleteUserCommand(id)));
        }
    }
}
