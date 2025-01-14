using Core.Entities;

namespace Core.Interaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductAsync();
    Task<Product?> GetProductByIdAsync(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);
    Task<bool>SaveChangesAsync();
    
}