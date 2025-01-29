using Core.Entities;
using Core.Interaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand,
     string? type,string? sort)
   {
    return Ok(await repo.ListAllAsync());
   } 

   [HttpGet("{id:int}")] // api/products/2
      public async Task<ActionResult<Product>> GetProduct(int id)  
    {
        var product = await repo.GetByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
{
    repo.Add(product);

    if (await repo.SaveAllAsync())
    {
        return CreatedAtAction("GetProduct", new {id = product.Id}, product);
    }

    return BadRequest("Problem creating product");
}

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct (int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
         return BadRequest("Cannot update this product");

         repo.Update(product);

         if (await repo.SaveAllAsync())
         {
            return NoContent();
         }
         return BadRequest("Problem updating product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await repo.GetByIdAsync(id);

        if (product == null) return NotFound();

        repo.Remove(product);

if (await repo.SaveAllAsync())
         {
            return NoContent();
         }
         return BadRequest("Problem deleteing product");

    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
 {
        // TODO: Implement method


    return Ok();
 }

     [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
 {
        // TODO: Implement method


    return Ok();
 }

    private bool ProductExists(int id)
    {
        return repo.Exists(id);
    }
} 
