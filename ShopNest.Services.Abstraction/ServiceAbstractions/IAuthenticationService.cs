using ShopNest.Shared.CommonResponses;
using ShopNest.Shared.DTOs.AuthenticationIdentityDTOS;

namespace ShopNest.Services.Abstraction.ServiceAbstractions
{
    public interface IAuthenticationService
    {
        //Takes Email, Password , DisplayName , UserName , PhoneNumber => Token, DisplayName ,Email
        Task<Result<UserDTO>> RegisterAsync(RegisterDTO registerDTO);
        //takes Email and Password an returns Token ,DisplayName ,Email
        Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO);
    }
}
