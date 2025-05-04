using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace IMS_InventoryManagmentSystem_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }


        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                var productAdded = await _productService.AddProductAsync(product);
                return CreatedAtAction(nameof(GetProductById), new { id = productAdded.Id }, productAdded);

            }
            catch(ArgumentException ex) 
            {
                return BadRequest(new {error=ex.Message});
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {

            if(id!=product.Id)
            {
                return BadRequest("Id Missmatch");
            }
            try
            {
                var productToBeUpdated = await _productService.UpdateProductAsync(product);
                return Ok(productToBeUpdated);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(new {error=ex.Message});
            }

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


    }
}
