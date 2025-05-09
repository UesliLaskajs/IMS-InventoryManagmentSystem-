using IMS_InventoryManagmentSystem_.Data;
using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IMS_InventoryManagmentSystem_.Repositories
{
    public class WarehouseRepository : IWareHouseRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<WarehouseRepository> _logger;

        public WarehouseRepository(ApplicationDbContext dbContext, ILogger<WarehouseRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // Add a new warehouse
        public async Task<WareHouse> AddWarehoues(WareHouse warehouse)
        {
            try
            {
                await _dbContext.Set<WareHouse>().AddAsync(warehouse);
                await _dbContext.SaveChangesAsync();
                return warehouse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding warehouse");
                throw;
            }
        }

        // Delete a warehouse
        public async Task DeleteProductAsync(WareHouse warehouse)
        {
            try
            {
                var existingWarehouse = await _dbContext.Set<WareHouse>().FindAsync(warehouse.Id);
                if (existingWarehouse != null)
                {
                    _dbContext.Set<WareHouse>().Remove(existingWarehouse);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _logger.LogError("Warehouse not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting warehouse");
                throw;
            }
        }

        // Get all warehouses
        public async Task<IEnumerable<WareHouse>> GetAllWareHouseAsync()
        {
            try
            {
                return await _dbContext.Set<WareHouse>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all warehouses");
                throw;
            }
        }

        // Get a specific warehouse by ID
        public async Task<WareHouse> GetWareHouseAsync(int id)
        {
            try
            {
                var warehouse = await _dbContext.Set<WareHouse>().FindAsync(id);
                if (warehouse == null)
                {
                    _logger.LogWarning($"Warehouse with ID {id} not found.");
                }
                return warehouse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching warehouse by ID");
                throw;
            }
        }

        // Save changes to the database
        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving changes");
                throw;
            }
        }

        // Update an existing warehouse
        public async Task<WareHouse> UpdateWareHouse(WareHouse warehouse)
        {
            try
            {
                var existingWarehouse = await _dbContext.Set<WareHouse>().FindAsync(warehouse.Id);
                if (existingWarehouse != null)
                {
                    _dbContext.Entry(existingWarehouse).CurrentValues.SetValues(warehouse);
                    await _dbContext.SaveChangesAsync();
                    return warehouse;
                }
                else
                {
                    _logger.LogWarning($"Warehouse with ID {warehouse.Id} not found for update.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating warehouse");
                throw;
            }
        }
    }
}
