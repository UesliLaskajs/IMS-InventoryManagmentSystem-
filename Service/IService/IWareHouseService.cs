using IMS_InventoryManagmentSystem_.Models;

namespace IMS_InventoryManagmentSystem_.Service.IService
{
    public interface IWareHouseService
    {
        Task<IEnumerable<WareHouse>> GetAllWareHouseAsync();

        Task<WareHouse> GetWareHouseAsync(int id);

        Task<WareHouse> AddWareHouseAsync(WareHouse Warehouse);

        Task<WareHouse > UpdateWarehouseAsync(WareHouse wareHouse);
        Task DeleteWarehouseAsync(WareHouse warehouse);

        Task SaveChanges();
    }
}
