using Microsoft.AspNetCore.Mvc;
using ProjectRefit.Input;
using ProjectRefit.Interface.Service;

namespace ProjectRefit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(InputAutenticateUser inputAutenticateUser)
        {
            try
            {
                await _userService.Login(inputAutenticateUser);
                return Ok("Usuario logado!");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var user = await _userService.GetUser();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
