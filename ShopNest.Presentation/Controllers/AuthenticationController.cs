using Microsoft.AspNetCore.Mvc;
using ShopNest.Shared.DTOs.AuthenticationIdentityDTOS;

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
    }
}
