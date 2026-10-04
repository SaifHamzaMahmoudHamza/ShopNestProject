using Microsoft.AspNetCore.Mvc;
using ShopNest.Presentation.Attributes;
using ShopNest.Services.Abstraction.Services;
using ShopNest.Shared;
using ShopNest.Shared.DTOs.ProductDTOs;

namespace ShopNest.Presentation.Controllers
{
    public class ProductsController(IProductService productService) : ApiBaseController
    {
        #region GetAllWithRedis
        //GetALL
        [HttpGet]
        [RedisCache(5)]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery] ProuctQueryParams QueryParams)
        {
            var products = await productService.GetAllProductsAsync(QueryParams);
            return Ok(products);
        }
        #endregion

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById([FromRoute] int id)
        {
            var result = await productService.GetProductByIdAsync(id)!;
            return HandleResult<ProductDTO>(result);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrandDTO>>> GetAllBrands()
        {
            var Brands = await productService.GetAllBrandsAsync();
            return Ok(Brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetAllTypes()
        {
            var Types = await productService.GetAllTypesAsync();
            return Ok(Types);
        }

    }
}
