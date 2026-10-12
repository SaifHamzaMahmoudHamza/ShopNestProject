using System.ComponentModel.DataAnnotations;

namespace ShopNest.Shared.DTOs.AuthenticationIdentityDTOS
{
    public record RegisterDTO([EmailAddress] string Email, string DisplayName, string Password, string UserName, [Phone] string PhoneNumber);
}
