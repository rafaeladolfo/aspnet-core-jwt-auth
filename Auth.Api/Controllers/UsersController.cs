using Auth.Core.DTO;
using Auth.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserBO _userBO;

        public UsersController(IUserBO userBO)
        {
            _userBO = userBO;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDTO userParam)
        {
            var user = _userBO.Authenticate(userParam.Login, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userBO.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userBO.Get(id);

            if (user == null)
                return BadRequest(new { message = "User not found" });

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpGet("test")]
        public IActionResult Test()
        {
            return (Ok("OK"));
        }
    }
}
