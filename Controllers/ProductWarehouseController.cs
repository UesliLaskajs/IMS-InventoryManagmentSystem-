using IMS_InventoryManagmentSystem_.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace IMS_InventoryManagmentSystem_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductWarehouseController : ControllerBase
    {
        private readonly IProductWareHouseService _productWareHouseService;
        private readonly ILogger<ProductWarehouseController> _logger;

        public ProductWarehouseController(IProductWareHouseService productWareHouseService, ILogger<ProductWarehouseController> logger)
        {
            _logger = logger;
            _productWareHouseService = productWareHouseService;
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateOrAddProductWareHouse(int warehouseId, int productId, int quantity)
        {
            try
            {
                await _productWareHouseService.AddOrUpdateAsync(warehouseId, productId, quantity);
                return Ok(new { message = "Product added or updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating/adding product to warehouse.");
                return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductWareHouse(int id)
        {
            try
            {
                var productWarehouse = await _productWareHouseService.GetProductWarehouseAsync(id);//I got the WarehouseProductEntity

                if (productWarehouse == null || !productWarehouse.Any())
                {
                    return NotFound(new { error = "Warehouse Not Found or Does Not Have Any" });
                }

                var result = new //Creating new Names for warehouse
                {
                    warehouseId=id,
                    location=productWarehouse.First().WareHouse?.Location,//displaying Locations
                    products = productWarehouse.Select(pw => new //looping throug inclusions on service for every product on the warehouse
                    {
                        productId = pw.productId,
                        productName=pw.Product?.Name,
                        quantity=pw.Quantity
                    })
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving product warehouse data.");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }

        }
    } }
