using IMS_InventoryManagmentSystem_.Models;

namespace IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo
{
    public interface IProductWarehouseRepository
    {
        Task AddOrUpdateAsync(int warehouseId, int productId, int quantity);

        Task<List<ProductWareHouse>> GetProductWarehouseAsync(int warehouseId);

    }
}
