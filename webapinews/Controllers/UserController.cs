using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapinews.Entities;
using webapinews.Helpers;
using webapinews.Interface;
using webapinews.Models;
using webapinews.Reporistory;
using webapinews.Services;
using AllowAnonymousAttribute = webapinews.Services.AllowAnonymousAttribute;
using AuthorizeAttribute = webapinews.Services.AuthorizeAttribute;

namespace webapinews.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserReporistory _userReporsitory;
        private IIdentityService _identityService;

        public UserController(IUserReporistory userReporsitory, IIdentityService identityService)
        {
            _userReporsitory = userReporsitory;
            _identityService = identityService;
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Login(UserDataRequest model)
        {
            var response = _userReporsitory.Authenticate(model);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Registor(User model)
        {

            _userReporsitory.Register(model);
            return Ok(new { message = "Registration successful" });
        }
        [Authorize(Role.Admin)]
        [HttpGet("[action]")]
        public IActionResult Get()
        {
            var users = _userReporsitory.GetAll();
            return Ok(users);
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<User>>> Get([FromQuery] OwnerStringParameter ownerStringParameter)
        {
            if (_userReporsitory == null)
            {
                return NotFound("No Data found");
            }
            var model = _userReporsitory.Get(ownerStringParameter);
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
        [Authorize(Role.Admin)]
        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            //only admins can access other user records
            var currentUser = _identityService.GetUserId();
            if (id != currentUser)
                return Unauthorized(new { message = "Unauthorized" });

            var user = _userReporsitory.GetById(id);
            return Ok(user);
        }
        [Authorize(Role.Admin)]
        [HttpPut("update")]
        public IActionResult Update(int id, User model)
        {
           if(model == null)
            {
                return Ok(new { message = "User Id not Found" });
            }
            model.Id = id;
            _userReporsitory.Update(model);
            return Ok(new { message = "User updated successfully" });
        }
        [Authorize(Role.Admin)]
        [HttpDelete("[action]")]
        public IActionResult Delete(int id) {
            if(_userReporsitory == null)
            {
                return BadRequest(new { message = "User NOT FOUND" });
            }
           _userReporsitory.Delete(id);
            return Ok(new { message = "User deleted successfully" });

        }
    }
}
