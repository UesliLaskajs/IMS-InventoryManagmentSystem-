using IMS_InventoryManagmentSystem_.Models.DTO;
using IMS_InventoryManagmentSystem_.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace IMS_InventoryManagmentSystem_.Controllers
{

    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]         
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO request)
        {
            var user = await _userService.RegisterAsync(request);

            if (user == null) {
                return BadRequest("User cannot Register");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto request)
        {
            var user = await _userService.LoginAsync(request);

            if (user == null) {
                return BadRequest("Error Logining In");
            }
            return Ok(user);
        }


        //To Continue With Authorize
    }
}
