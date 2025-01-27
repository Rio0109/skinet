using Core.Entities;
using Core.Interaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }
    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }
    public async Task<Product?> GetProductAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }
    public async Task<IReadOnlyList<Product>> GetProductAsync()
    {
        return  await context.Products.ToListAsync();
    }
    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }
    public bool ProductExists(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}