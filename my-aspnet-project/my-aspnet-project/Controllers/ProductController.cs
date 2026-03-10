using Microsoft.AspNetCore.Mvc;
using my_aspnet_project.Models;

namespace my_aspnet_project.Controllers;

[ApiController]
[Route("api/[Controller]")]

public class ProductController : ControllerBase
{
    private static List<ProductModel> products = new()
    {
        new ProductModel { Id = 1, Name = "Iphone 17 Pro", Price = 1000, Category = "Electronics" },
        new ProductModel { Id = 2, Name = "Macbook Pro 15' M5", Price = 2500, Category = "Electronics" },
        new ProductModel { Id = 3, Name = "Porsche 911 Targa 4S", Price = 150000, Category = "Cars" },
    };
    
    [HttpGet]
    public IActionResult GetAll(int page = 1, int pageSize = 10, string? sort = null)
    {
        var query = products.AsQueryable();

        if (sort == "name")
            query = query.OrderBy(p => p.Name);

        if (sort == "price")
            query = query.OrderBy(p => p.Price);

        var result = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Ok(result);
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet("slug/{slug:minlength(3)}")]
    public IActionResult GetBySlug(string slug)
    {
        var product = products.FirstOrDefault(p=>p.Slug == slug);
        
        if (product == null)
            return NotFound();
        return Ok(product);
    }

    [HttpGet("search/{name}")]
    public IActionResult GetByName(string name)
    {
        var result = products
            .Where(p =>p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();
        
        return Ok(result);
    }
    
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, ProductModel updatedProduct)
    {
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        product.Name = updatedProduct.Name;
        product.Price = updatedProduct.Price;
        product.Category = updatedProduct.Category;
        product.Slug = updatedProduct.Slug;

        return Ok(product);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);

        if (product == null)
            return NotFound();

        products.Remove(product);

        return NoContent();
    }
}