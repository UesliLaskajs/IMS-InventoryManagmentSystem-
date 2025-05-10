using IMS_InventoryManagmentSystem_.Data;
using IMS_InventoryManagmentSystem_.Models;
using IMS_InventoryManagmentSystem_.Repositories.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

public class ProductRepository : IProductRepo
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(ApplicationDbContext context, ILogger<ProductRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProductAsync(Product product)
    {
        var productToBeDeleted = await _context.Products.FindAsync(product.Id);
        if (productToBeDeleted != null)
        {
            _context.Products.Remove(productToBeDeleted);
            await _context.SaveChangesAsync();
        }
        else
        {
            _logger.LogError("Product to be DELETED is Not Found");
        }
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetProductAsync(int productId)
    {
        var productToBeFound = await _context.Products.FirstOrDefaultAsync(u => u.Id == productId);
        if (productToBeFound == null)
        {
            throw new KeyNotFoundException($"Product with ID {productId} was not found.");
        }
        return productToBeFound;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        var existingProduct = await _context.Products.FindAsync(product.Id);

        if (existingProduct == null)
        {
            _logger.LogError($"Product with ID {product.Id} not found.");
            return null; // Or throw an exception if needed
        }

        // Map updated values from the incoming product to the existing one
        existingProduct.Name = product.Name; // example property
        existingProduct.Price = product.Price; // example property
                                               // ... map all other properties as needed

        // You can use Entity Framework to track changes and automatically handle the update
        _context.Products.Update(existingProduct); // Make sure you're updating the tracked entity
        await _context.SaveChangesAsync();

        return existingProduct; // Returning the updated entity
    }
}
    