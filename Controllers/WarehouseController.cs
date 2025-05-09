using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Service;
using IMS_InventoryManagmentSystem_.Service.IService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IMS_InventoryManagmentSystem_.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWareHouseService _warehouseService;
        private readonly ILogger<WarehouseController> _logger;

        public WarehouseController(IWareHouseService wareHouseService, ILogger<WarehouseController> logger)
        {
            _warehouseService = wareHouseService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> GetallWarehouses() {
            var warehouse = await _warehouseService.GetAllWareHouseAsync();

            return Ok(warehouse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWareHouse(int id)
        {
            try
            {
                var warehouse = await _warehouseService.GetWareHouseAsync(id);
                return Ok(warehouse);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex) {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddWarehouse([FromBody] WareHouse wareHouse)
        {
            try
            {
                var wareHouseAdded = await _warehouseService.AddWareHouseAsync(wareHouse);
                return CreatedAtAction(nameof(GetWareHouse), new { id = wareHouseAdded.Id }, wareHouseAdded);
            }
            catch (ArgumentException ex) {

                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex) {
                return Conflict(new { error = ex.Message });
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateWarehouse(int id, [FromBody] WareHouse wareHouse)
        {

            if (id != wareHouse.Id)
            {
                return BadRequest("Id Missmatch");
            }
            try
            {
                var wareHouseToBeUpdated = await _warehouseService.UpdateWarehouseAsync(wareHouse);
                return Ok(wareHouseToBeUpdated);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWareHouse(int id)
        {
            try
            {
                await _warehouseService.DeleteWarehouseAsync(id);
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
