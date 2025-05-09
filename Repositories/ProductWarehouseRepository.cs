using IMS_InventoryManagmentSystem_.Data;
using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace IMS_InventoryManagmentSystem_.Repositories
{
    public class ProductWarehouseRepository :IProductWarehouseRepository
    {
        private readonly ILogger<ProductWarehouseRepository> _logger;
        private readonly ApplicationDbContext _context;


        public ProductWarehouseRepository(ILogger<ProductWarehouseRepository> logger,ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddOrUpdateAsync(int warehouseId, int productId, int quantity)
        {
            var existingProductWarehouse = _context.ProductWareHouse.FirstOrDefault(pw=>pw.Id==warehouseId && pw.productId==productId);
            if (existingProductWarehouse != null) {
                existingProductWarehouse.Quantity = quantity;
            }
            else
            {
                var newEntry = new ProductWareHouse
                {
                    WareHouseId = warehouseId,
                    productId = productId,
                    Quantity = quantity,
                };
                await _context.ProductWareHouse.AddAsync(newEntry);
            }
            _context.SaveChangesAsync();
        }



        public async Task<List<ProductWareHouse>> GetProductWarehouseAsync(int warehouseId)
        {
            return await _context.ProductWareHouse.Where(pw => pw.WareHouseId == warehouseId)
                    .Include(pw => pw.Product).ToListAsync();

        }
    }
}
