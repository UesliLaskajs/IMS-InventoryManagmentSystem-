using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo;
using IMS_InventoryManagmentSystem_.Service.IService;

namespace IMS_InventoryManagmentSystem_.Service
{
    public class ProductWareHouseService : IProductWareHouseService
    {
        private readonly IProductWarehouseRepository _productWarehouseRepository;
        private readonly ILogger<IProductWareHouseService> _logger;

        public ProductWareHouseService(IProductWarehouseRepository productWarehouseRepository,ILogger<ProductWareHouseService> logger)
        {
            _productWarehouseRepository = productWarehouseRepository;
            _logger = logger;
        }

        public async Task AddOrUpdateAsync(int warehouseId, int productId, int quantity)
        {
            if (warehouseId < 0||productId<0||quantity<0) {
                throw new ArgumentException("Invalid Input Data");
            }
             await _productWarehouseRepository.AddOrUpdateAsync(warehouseId, productId, quantity);
        }

        public async Task<List<ProductWareHouse>> GetProductWarehouseAsync(int warehouseId)
        {
            if (warehouseId <= 0)
                throw new ArgumentException("Invalid warehouse ID.");

            return await _productWarehouseRepository.GetProductWarehouseAsync(warehouseId);
        }

    }
}
