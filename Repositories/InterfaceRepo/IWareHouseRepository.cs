using IMS_InventoryManagmentSystem_.Models;

namespace IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo
{
    public interface IWareHouseRepository
    {
        Task <WareHouse> GetWareHouseAsync (int id);
        Task <IEnumerable<WareHouse>> GetAllWareHouseAsync();

        Task<WareHouse> AddWarehoues(WareHouse warehouse);

        Task<WareHouse> UpdateWareHouse(WareHouse warehouse);

        Task DeleteProductAsync(WareHouse warehouse);

        Task SaveChangesAsync();
    }
}
