using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo;
using IMS_InventoryManagmentSystem_.Service.IService;

namespace IMS_InventoryManagmentSystem_.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly ILogger<ProductService> _logger;
      

        public ProductService(IProductRepo productRepo,ILogger<ProductService> logger)
        {
            _productRepo = productRepo;
            _logger = logger;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentException(nameof(product));
            }

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product Name cannot be empty");
            }

            if (product.Price < 0)
            {
                throw new ArgumentException("Price cannot be lower than 0");
            }

            var existingProducts = await _productRepo.GetAllProductsAsync();
            if (existingProducts.Any(p => p.Name == product.Name))
                throw new InvalidOperationException("A product with this name already exists.");

            return await _productRepo.AddProductAsync(product);


        }

        public async Task DeleteProductAsync(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid product ID.");

            var product = await _productRepo.GetProductAsync(productId);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {productId} not found for deletion.");
                throw new KeyNotFoundException($"Product with ID {productId} not found.");
            }

            await _productRepo.DeleteProductAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _productRepo.GetAllProductsAsync();
            if (!products.Any())
            {
                _logger.LogInformation("No products found.");
            }

            return products;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid product ID.");

            var product = await _productRepo.GetProductAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found.");
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            return product;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (product.Id <= 0)
                throw new ArgumentException("Invalid product ID.");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be empty.");

            if (product.Price <= 0)
                throw new ArgumentException("Product price must be greater than zero.");

            var existingProduct = await _productRepo.GetProductAsync(product.Id);
            if (existingProduct == null)
            {
                _logger.LogWarning($"Product with ID {product.Id} not found for update.");
                throw new KeyNotFoundException($"Product with ID {product.Id} not found.");
            }

            return await _productRepo.UpdateProductAsync(product);
        }
    }
}
