using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapinews.Entities;
using webapinews.Interface;
using webapinews.Models;
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

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Login(UserDataRequest model)
        {
            var response = _userService.Authenticate(model);
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Registor(User model)
        {
           
         _userService.Register(model);
            return Ok(new { message = "Registration successful" });
        }
        [Authorize(Role.Admin)]
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
        
        [HttpGet("{Id}")]
        public IActionResult GetById(int id)
        {
            //only admins can access other user records
           var currentUser = (User)HttpContext.Items["User"];
            if (id != currentUser.Id && currentUser.Role != Role.Admin)
                return Unauthorized(new { message = "Unauthorized" });

            var user = _userService.GetById(id);
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
            _userService.Update(model);
            return Ok(new { message = "User updated successfully" });
        }
        [Authorize(Role.Admin)]
        [HttpDelete("[action]")]
        public IActionResult Delete(int id) {
            _userService.Delete(id);
            return Ok(new { message = "User deleted successfully" });

        }
    }
}
