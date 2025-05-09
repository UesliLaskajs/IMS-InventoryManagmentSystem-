using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Service.IService;

namespace IMS_InventoryManagmentSystem_.Service
{
    public class ProductWareHouseService : IProductWareHouseService
    {
        private readonly IProductWareHouseService _productWareHouseService;
        private readonly ILogger<IProductWareHouseService> _logger;

        public ProductWareHouseService(IProductWareHouseService productWareHouseService,ILogger<ProductWareHouseService> logger)
        {
            _productWareHouseService = productWareHouseService;
            _logger = logger;
        }
        public Task<ProductWareHouse> GetProductWithWarehouseAsync(int warehouseId)
        {
            throw new NotImplementedException();
        }
    }
}
