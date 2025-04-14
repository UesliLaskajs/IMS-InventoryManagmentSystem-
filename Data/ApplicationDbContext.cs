using IMS_InventoryManagmentSystem_.Models;
using Microsoft.EntityFrameworkCore;

namespace IMS_InventoryManagmentSystem_.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<User> User { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<WareHouse> WareHouse { get; set; }
    }
}
