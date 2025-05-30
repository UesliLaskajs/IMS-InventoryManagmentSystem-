﻿using IMS_InventoryManagmentSystem_.Models;

namespace IMS_InventoryManagmentSystem_.Service.IService
{
    public interface IProductWareHouseService
    {
        Task AddOrUpdateAsync(int warehouseId, int productId, int quantity);
        Task <List<ProductWareHouse>> GetProductWarehouseAsync(int warehouseId);
    }
}
