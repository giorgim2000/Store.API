using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.API.Models;
using Store.API.Services;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        const int maxPageSize = 20;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }
        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts(int pageSize = 10, int pageNumber = 1)
        {
            if(pageSize > maxPageSize)
                pageSize = maxPageSize;
            return Ok(await _productService.GetAllProducts(pageSize, pageNumber));
        }
        [HttpGet("{productId}")]
        public async Task<ActionResult<ProductDto?>> GetProductById(int productId)
        {
            var product = await _productService.GetProductById(productId);
            if(product == null)
                return NotFound();
            else
                return Ok(product);
        }
        [HttpPost]
        public async Task<ActionResult<ProductForInputDto?>> CreateProductAsync(ProductForInputDto product)
        {
            var res = await _productService.CreateProductAsync(product);
            if (res == null)
                return BadRequest();
            else
                return Ok(res);
        }

        [HttpPut(nameof(UpdateProduct))]
        public async Task<ActionResult> UpdateProduct(ProductDto input)
        {
            var res = await _productService.UpdateProductAsync(input);
            if (res)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("{productId}")]
        public async Task<ActionResult> ChangeProductPrice(int productId, int newPrice)
        {
            var res = await _productService.ChangeProductPrice(productId, newPrice);
            if (res)
                return Ok();
            else
                return BadRequest();
        }
        [HttpPut("{productId}/{newQty}")]
        public async Task<ActionResult> ChangeProductQty(int productId, int newQty)
        {
            var res = await _productService.ChangeProductQty(productId, newQty);
            if (res)
                return NoContent();
            else
                return BadRequest();
        }
        [HttpDelete]
        public async Task<ActionResult> RemoveProduct(int productId)
        {
            var res = await _productService.DeleteProductById(productId);
            if (res)
                return NoContent();
            else
                return NotFound();
        }

    }
}
