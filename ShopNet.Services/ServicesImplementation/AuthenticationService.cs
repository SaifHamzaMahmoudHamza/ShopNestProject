using Microsoft.AspNetCore.Identity;
using ShopNest.Domain.Entities.IdentityModule;
using ShopNest.Services.Abstraction.ServiceAbstractions;
using ShopNest.Shared.CommonResponses;
using ShopNest.Shared.DTOs.AuthenticationIdentityDTOS;

namespace ShopNet.Services.ServicesImplementation
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager) : IAuthenticationService
    {
        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null)
                return Error.InvalidCredentials("User.InvalidCredentials");
            var passwordCheck = await userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!passwordCheck)
            {
                return Error.InvalidCredentials("User.InvalidCredentials");
            }
            return new UserDTO(user.DisplayName, user.Email!, "Token");
        }

        public async Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser()
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.UserName
            };
            var IdentityResult = await userManager.CreateAsync(user, registerDTO.Password);
            if (IdentityResult.Succeeded)
                return new UserDTO(user.DisplayName, user.Email!, "Token");
            return IdentityResult.Errors.Select(E => Error.Validation(E.Code, E.Description)).ToList();
        }
    }
}
