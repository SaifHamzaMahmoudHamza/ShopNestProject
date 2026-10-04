using AutoMapper;
using ShopNest.Domain.Contracts;
using ShopNest.Domain.Entities.ProductModule;
using ShopNest.Services.Abstraction.Services;
using ShopNest.Shared;
using ShopNest.Shared.CommonResponses;
using ShopNest.Shared.DTOs.ProductDTOs;
using ShopNet.Services.Specifications.ProductSpecifications;

namespace ShopNet.Services.ServicesImplementation
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
    {

        public async Task<IEnumerable<ProductBrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            return mapper.Map<IEnumerable<ProductBrandDTO>>(Brands);
        }
        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProuctQueryParams queryParams)
        {
            // Get repository
            var repo = unitOfWork.GetRepository<Product, int>();

            // Build specification
            var specs = new ProductWithTypeAndBrandSpecification(queryParams);
            // Build count spec
            var specCount = new ProductCountSpecification(queryParams);

            // Fetch products
            var Products = await repo.GetAllAsync(specs);
            // Return paginated result
            return new PaginatedResult<ProductDTO>(
                queryParams.PageIndex,
                mapper.Map<IEnumerable<ProductDTO>>(Products).Count(),
                await repo.CountAsync(specCount),
                mapper.Map<IEnumerable<ProductDTO>>(Products)
            );
        }
        public async Task<IEnumerable<ProductTypeDTO>> GetAllTypesAsync()
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return mapper.Map<IEnumerable<ProductTypeDTO>>(Types);
        }
        public async Task<Result<ProductDTO>>? GetProductByIdAsync(int id)
        {
            var Specs = new ProductWithTypeAndBrandSpecification(id);
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(Specs);
            if (Product is null)
                Error.NotFound("Product.NotFound", $"Product with Id {id} is Not Found");
            return mapper.Map<ProductDTO>(Product);

        }
    }
}
