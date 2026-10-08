using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopNest.API.Extensions;
using ShopNest.API.Factories;
using ShopNest.Domain.Contracts;
using ShopNest.Domain.Contracts.Initialization;
using ShopNest.Domain.Contracts.RepositoryAbstraction;
using ShopNest.Domain.Contracts.RepositoryAbstraction.BasketRepositoryAbstraction;
using ShopNest.Presistence.Data.DataSeeding;
using ShopNest.Presistence.Data.DbContexts;
using ShopNest.Presistence.IdentityData.DbContext;
using ShopNest.Presistence.Repositories;
using ShopNest.Presistence.Repositories.BasketRepositories;
using ShopNest.Services.Abstraction.ServiceAbstractions;
using ShopNest.Services.Abstraction.Services;
using ShopNet.Services.MappingProfiles.PictureResolve;
using ShopNet.Services.ServicesImplementation;
using StackExchange.Redis;
using ExceptionHandlerMiddleware = ShopNest.API.CustomMiddlwares.ExceptionHandlerMiddleware;

namespace ShopNest.API
{
    public class Program
    { //SEPERATION OF CONCERNS AND OPEN FOR EXTENSION CLOSE FOR MODIFICATION
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Services Registration

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDataInitializer, DataInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<ICacheRepository, CacheRepository>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            });
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            });
            //Imapper 15 License in production
            //builder.Services.AddAutoMapper(l => l.LicenseKey = "", typeof(ProductProfile).Assembly);
            //Imapper 14 No License Required in Production niether development
            builder.Services.AddAutoMapper(typeof(ServiceAssemblyMarker).Assembly);
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            #endregion
            #region DI Container Building -> IService Provider
            var app = builder.Build();
            await app.MigrateDataBaseAsync<StoreIdentityDbContext>();
            await app.MigrateDataBaseAsync<StoreDbContext>();
            await app.SeedDataAsync();
            #endregion

            #region Pipeline Configuration -> Middlewares Sequence
            //_ = app.Use(async (context, next) =>
            //{
            //    try
            //    {
            //        await next();
            //    }
            //    catch (Exceptions ex)
            //    {
            //        Console.WriteLine(ex.Message);//Logg in Console
            //        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //        await context.Response.WriteAsJsonAsync(new
            //        {
            //            StatusCode = StatusCodes.Status500InternalServerError,
            //            Error = $"Unexpected Error {ex.Message}"
            //        });
            //    }
            //});

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            await app.RunAsync();
        }
    }
}
