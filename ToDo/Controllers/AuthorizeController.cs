using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Models.DTOs;
using ToDo.Services;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly UserService userService;
        private readonly TokenService tokenService;

        public AuthorizeController(UserService userService, TokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register([FromBody] AuthDTO authDTO)
        {
            ResponseMessage responseMessage = userService.RegisterUser(authDTO, out bool isValid);
            if (!isValid)
            {
                return BadRequest(responseMessage);
            }
            return Ok(responseMessage);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] AuthDTO authDTO)
        {
            ResponseMessage responseMessage = tokenService.LoginUser(authDTO, out bool isValid);
            if (!isValid)
            {
                return BadRequest(responseMessage);
            }
            return Ok(responseMessage);
        }
    }
}
