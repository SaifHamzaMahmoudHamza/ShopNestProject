using Microsoft.EntityFrameworkCore;
using ShopNest.Domain.Contracts.Initialization;

namespace ShopNest.API.Extensions
{
    public static class WebApplicationRegister
    {
        //Approach 1
        //public async static Task<WebApplication> MigrateDataBaseAsync(this WebApplication app)
        //{
        //    await using var scope = app.Services.CreateAsyncScope();
        //    var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
        //    var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        //    if (pendingMigrations.Any())
        //    {
        //        await dbContext.Database.MigrateAsync();
        //    }
        //    return app;

        //}
        //public async static Task<WebApplication> MigrateIdentityDataBaseAsync(this WebApplication app)
        //{
        //    await using var scope = app.Services.CreateAsyncScope();
        //    var identityDbContext = scope.ServiceProvider.GetRequiredService<StoreIdentityDbContext>();
        //    var pendingMigrations = await identityDbContext.Database.GetPendingMigrationsAsync();
        //    if (pendingMigrations.Any())
        //    {
        //        await identityDbContext.Database.MigrateAsync();
        //    }
        //    return app;

        //}
        //Approach 2
        public async static Task<WebApplication> MigrateDataBaseAsync<Context>(this WebApplication app)
            where Context : DbContext
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<Context>();
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync();
            }
            return app;

        }
        public async static Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var DataInitializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            await DataInitializer.InitializeAsync();
            return app;

        }
        //public static WebApplication ServiceRegistrations(string[] args)
        //{
        //    var builder = WebApplication.CreateBuilder(args);

        //    // Add services to the container.

        //    builder.Services.AddControllers();
        //    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //    builder.Services.AddEndpointsApiExplorer();
        //    builder.Services.AddSwaggerGen();
        //    builder.Services.AddScoped<IDataInitializer, DataInitializer>();
        //    builder.Services.AddDbContext<StoreDbContext>(options =>
        //    {
        //        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        //    });

        //}
    }
}
