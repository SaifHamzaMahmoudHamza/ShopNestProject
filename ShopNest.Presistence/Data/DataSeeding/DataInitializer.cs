using ShopNest.Domain.Contracts.Initialization;
using ShopNest.Domain.Entities;
using ShopNest.Domain.Entities.OrderModule;
using ShopNest.Presistence.Data.DbContexts;
using System.Text.Json;
namespace ShopNest.Presistence.Data.DataSeeding
{
    public class DataInitializer(StoreDbContext storeDbContext) : IDataInitializer
    {
        public async Task InitializeAsync()
        {
            try
            {
                var HasProducts = await storeDbContext.Products.AnyAsync();
                var HasBrands = await storeDbContext.ProductBrands.AnyAsync();
                var HasTypes = await storeDbContext.ProductTypes.AnyAsync();
                var HasDelivery = await storeDbContext.DeliveryMethods.AnyAsync();
                if (HasBrands && HasProducts && HasTypes && HasDelivery)
                    return;
                if (!HasBrands)
                    await SeedDataFromJson<ProductBrand, int>("brands.json", storeDbContext.ProductBrands);

                if (!HasTypes)
                    await SeedDataFromJson<ProductType, int>("types.json", storeDbContext.ProductTypes);

                await storeDbContext.SaveChangesAsync();

                if (!HasProducts)
                    await SeedDataFromJson<Product, int>("products.json", storeDbContext.Products);

                if (!HasDelivery)
                    await SeedDataFromJson<DeliveryMethod, int>("delivery.json", storeDbContext.DeliveryMethods);
                await storeDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Data Initialization {ex}");
            }

        }
        #region Helper-Methods
        public async static Task SeedDataFromJson<T, TKey>(string fileName, DbSet<T> Entity) where T : BaseEntity<TKey>
        {
            //D:\Route -.Net\C#\ShopNest\ShopNest.Presistence\Data\DataSeeding\JsonFiles\
            var filePath = @"..\ShopNest.Presistence\Data\DataSeeding\JsonFiles\" + fileName;
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Json File Not Found", filePath);
            }
            try
            {
                using var dataStream = File.OpenRead(filePath);
                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if (data is not null)
                {
                    await Entity.AddRangeAsync(data);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Seeding Data from JSON {ex}");
            }
        }
        #endregion
    }
}
