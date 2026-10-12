using System.ComponentModel.DataAnnotations;

namespace ShopNest.Shared.DTOs.AuthenticationIdentityDTOS
{
    public record LoginDTO([EmailAddress] string Email, string Password);
}
