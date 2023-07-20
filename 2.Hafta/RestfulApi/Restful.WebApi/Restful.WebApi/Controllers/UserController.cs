using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restful.WebApi.DAL.Entities;
using Restful.WebApi.FakeService.Abstract;

namespace Restful.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Users()
        {
            _userService.GetUsers();
            return Ok(_userService.GetUsers());
        }

        [HttpPut]
        public IActionResult Login([FromBody] User user)
        {
            _userService.LoginUser(user);
            return Ok();
        }
    }
}
