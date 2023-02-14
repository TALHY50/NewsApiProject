using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapinews.Command.User_Commands;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Models;
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
        [HttpPost("[action]")]
        public async Task<ActionResult<UserDataResponse>> Login(UserDataRequest model)
        {
            if(model == null )
            {
                return NotFound(new { message = "NO User Found" });
            }
            var response = await _mediator.Send(new loginCommand(model));
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult<User>> Registration(User model)
        {
            if(model == null)
            {
                return Ok(new { message = "Invalid Credential" });
            }
            await _mediator.Send(new RegistorUserCommand(model));
            return Ok(new { message = "Registration successful" });
        }
        [Authorize(Role.Admin)]
        [HttpGet("[action]")]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await _mediator.Send(new GetUserListQuery());
            return Ok(users);
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<User>>> Get([FromQuery] OwnerStringParameter ownerStringParameter)
        {
            if (ownerStringParameter == null)
            {
                return NotFound("Pagination Not Applied");
            }
            var model = await _mediator.Send(new GetUserPagenatedQuery(ownerStringParameter));
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
            return Ok(metadata);
        }
        [Authorize(Role.Admin)]
        [HttpGet("{Id}")]
        public async Task<ActionResult<User>> GetById(int id)
        {
            //only admins can access other user records
            var currentUser = _identityService.GetUserId();
            if (id != currentUser)
                return Unauthorized(new { message = "Unauthorized" });
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(user);
        }
        [Authorize(Role.Admin)]
        [HttpPut("update")]
        public async Task<ActionResult<User>> Update(int id, User model)
        {
           if(model == null)
            {
                return Ok(new { message = "User Id not Found" });
            }
            model.Id = id;
            await _mediator.Send(new UpdateUserCommand(model));
            return Ok(new { message = "User updated successfully" });
        }
        [Authorize(Role.Admin)]
        [HttpDelete("[action]")]
        public async Task<ActionResult<User>> Delete(int id) {
            var currentUser = _identityService.GetUserId();
            if (id != currentUser)
            return Unauthorized(new { message = "Unauthorized" });
            await  _mediator.Send(new DeleteUserCommand(id));
            return Ok(new { message = "User deleted successfully" });

        }
    }
}
