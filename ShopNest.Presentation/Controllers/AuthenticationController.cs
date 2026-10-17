using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopNest.Shared.DTOs.AuthenticationIdentityDTOS;
using System.Security.Claims;

namespace ShopNest.Presentation.Controllers
{
    public class AuthenticationController(Services.Abstraction.ServiceAbstractions.IAuthenticationService authenticationService) : ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var result = await authenticationService.LoginAsync(loginDTO);
            return HandleResult(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var result = await authenticationService.RegisterAsync(registerDTO);
            return HandleResult(result);
        }

        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmail(string Email)
        {
            var result = await authenticationService.EmailExistsAsync(Email);
            return Ok(result);
        }


        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> CurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await authenticationService.GetUserByEmailAsync(email!);
            return HandleResult(Result);
        }
    }
}
