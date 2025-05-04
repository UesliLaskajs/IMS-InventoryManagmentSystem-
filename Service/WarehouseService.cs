using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo;
using IMS_InventoryManagmentSystem_.Service.IService;

namespace IMS_InventoryManagmentSystem_.Service
{
    public class WarehouseService : IWareHouseService
    {
        private readonly IWareHouseService _warehouseService;
        private readonly ILogger<WarehouseService> _logger;
        public WarehouseService(IWareHouseService wareHouseService,ILogger<WarehouseService> logger)
        {
             _logger = logger;
            _warehouseService = wareHouseService;
        }
        public async Task<WareHouse> AddWareHouseAsync(WareHouse warehouse)
        {

            if (warehouse == null) {
                throw new ArgumentException(nameof(warehouse));
            }

            if (string.IsNullOrWhiteSpace(warehouse.Name))
            {
                throw new ArgumentException("Name Must not be Empty"); 
            }
            var existingWarehouses = await _warehouseService.GetAllWareHouseAsync();
            if (existingWarehouses.Any(w => w.Name.Equals(warehouse.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("A Warehouse with this name already exists.");
            }

            // Now add the warehouse using the repository or service layer, depending on your design.
            return await _warehouseService.AddWareHouseAsync(warehouse);
        }

        public async Task DeleteWarehouseAsync(WareHouse warehouse)
        {
            if (warehouse.Id == 0) {
                throw new ArgumentException("Invalid Warehouse id");
            }
            var wareHouse=await _warehouseService.GetWareHouseAsync(warehouse.Id);
            if (wareHouse == null) {
                _logger.LogWarning($"Warehouse with ID {wareHouse.Id} not found for deletion.");
                throw new KeyNotFoundException("Warehouse with ID {ware} not found.");
            }
        }

        public async Task<IEnumerable<WareHouse>> GetAllWareHouseAsync()
        {
           var warhouses= await _warehouseService.GetAllWareHouseAsync();
            if (!warhouses.Any())
            {
                 _logger.LogError("Error No Warheouses Found");
                throw new ArgumentException("No WareHouseFound");
            }
            return warhouses;
        }

        public async Task<WareHouse> GetWareHouseAsync(int id)
        {
            if (id == 0) {
                throw new ArgumentException("Id not found");
            }
            var warehouse = await _warehouseService.GetWareHouseAsync(id);
            if (warehouse == null)
            {
                _logger.LogError($"Warehouse with {id} not found");
                throw new KeyNotFoundException($"Warehouse with {id} not found");
            }
            return warehouse;
        }

        public Task SaveChanges()
        {
           return _warehouseService.SaveChanges();
        }

        public async Task<WareHouse> UpdateWarehouseAsync(WareHouse wareHouse)
        {
            if (wareHouse == null)
                throw new ArgumentNullException(nameof(wareHouse));

            if (wareHouse.Id <= 0)
                throw new ArgumentException("Invalid wareHouse ID.");

            if (string.IsNullOrWhiteSpace(wareHouse.Name))
                throw new ArgumentException("wareHouse name cannot be empty.");

            var existingwareHouse = await _warehouseService.GetWareHouseAsync(wareHouse.Id);
            if (existingwareHouse == null)
            {
                _logger.LogWarning($"Product with ID {wareHouse.Id} not found for update.");
                throw new KeyNotFoundException($"Product with ID {wareHouse.Id} not found.");
            }

            return await _warehouseService.UpdateWarehouseAsync(wareHouse);
        }
    }
}
