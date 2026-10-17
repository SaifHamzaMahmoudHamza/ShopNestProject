using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopNest.Domain.Entities.IdentityModule;
using ShopNest.Services.Abstraction.ServiceAbstractions;
using ShopNest.Shared.CommonResponses;
using ShopNest.Shared.DTOs.AuthenticationIdentityDTOS;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopNet.Services.ServicesImplementation
{
    public class AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration) : IAuthenticationService
    {
        public async Task<bool> EmailExistsAsync(string Email)
        {
            var User = await userManager.FindByEmailAsync(Email);
            //if (User is null)
            //    return false;
            //return true;
            return User != null;
        }

        public async Task<Result<UserDTO>> GetUserByEmailAsync(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email);
            if (user == null)
                return Error.NotFound("User.NotFound", $"User With Email : {Email} is not found");
            return new UserDTO(user.DisplayName, user.Email, await CreateTokenAsync(user));

        }

        public async Task<Result<UserDTO>> LoginAsync(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);
            if (user is null)
                return Error.InvalidCredentials("User.InvalidCredentials");
            var PasswordValid = await userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!PasswordValid)
                return Error.InvalidCredentials("User.InvalidCredentials");
            var Token = await CreateTokenAsync(user);
            return new UserDTO(user.DisplayName, user.Email!, Token);
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
            {
                var Token = await CreateTokenAsync(user);
                return new UserDTO(user.DisplayName, user.Email!, Token);
            }


            return IdentityResult.Errors.Select(E => Error.Validation(E.Code, E.Description)).ToList();
        }
        #region Helper Methods 

        private async Task<string> CreateTokenAsync(ApplicationUser applicationUser)
        {
            var Claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,applicationUser.Email!),
                new Claim(JwtRegisteredClaimNames.Name,applicationUser.UserName!),
            };
            var Roles = await userManager.GetRolesAsync(applicationUser);
            foreach (var Role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, Role));
            }
            var SecretKey = configuration["JWTOptions:SecretKey"];

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey!));

            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken
                (
                issuer: configuration["JWTOptions:Issuer"],
                audience: configuration["JWTOptions:Audience"],
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credentials,
                claims: Claims
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }

        #endregion
    }
}
