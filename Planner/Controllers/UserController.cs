using System.Runtime.InteropServices.JavaScript;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Planner.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] Login user)
        {
            try
            {
                user.Id = await _userService.CreateAccount(user);
            }
            catch
            {
                return Problem();
            }

            user.Password = "";

            return Created($"{user.Id}", user);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Login user)
        {
            var loginResult = await _userService.SingIn(user);
            return Ok(Results.Json(new { access_token = loginResult.JwtToken, username = loginResult.Name }));
        }
    }
}
