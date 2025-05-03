using IMS_InventoryManagmentSystem_.Models;

namespace IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo
{
    public interface IProductRepo
    {
        Task<Product> GetProductAsync(int  productId);
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product> AddProductAsync(Product product);

        Task<Product> UpdateProductAsync(Product product);

        Task DeleteProductAsync(Product product);

        Task SaveChangesAsync();
    }
}
