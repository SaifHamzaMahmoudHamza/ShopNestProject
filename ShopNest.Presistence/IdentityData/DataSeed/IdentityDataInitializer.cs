using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ShopNest.Domain.Contracts.Initialization;
using ShopNest.Domain.Entities.IdentityModule;

namespace ShopNest.Presistence.IdentityData.DataSeed
{
    public class IdentityDataInitializer(UserManager<ApplicationUser> userManager
        , RoleManager<IdentityRole> roleManager
        , ILogger<IdentityDataInitializer> logger) : IDataInitializer
    {
        //call Users Table
        //call Roles Table
        //Assign The Role To The User 
        public async Task InitializeAsync()
        {

            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!userManager.Users.Any())
                {
                    var User2 = new ApplicationUser()
                    {
                        DisplayName = "Ahmed Ali",
                        UserName = "AhmedAli",
                        Email = "ahmedali246@gmail.com",
                        PhoneNumber = "01033659273"
                    };
                    var User1 = new ApplicationUser()
                    {
                        DisplayName = "Saif Hamza",
                        UserName = "SaifHamza",
                        Email = "saifhamza246@gmail.com",
                        PhoneNumber = "01033659272"
                    };

                    await userManager.CreateAsync(User1, "P@ssw0rd");
                    await userManager.CreateAsync(User2, "P@ssw0rd");

                    await userManager.AddToRoleAsync(User2, "Admin");
                    await userManager.AddToRoleAsync(User1, "SuperAdmin");


                }


            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error Ocurred internally while seeding the DataBase");
            }

        }
    }
}
