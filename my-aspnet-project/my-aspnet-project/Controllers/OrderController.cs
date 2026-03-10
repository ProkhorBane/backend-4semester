using Microsoft.AspNetCore.Mvc;
using my_aspnet_project.Models;

namespace my_aspnet_project.Controllers;


[ApiController]
[Route("api/[controller]")]

public class OrderController : ControllerBase
{
    
    private static List<OrderModel> orders = new()
    {
        new OrderModel
        {
            Id = Guid.NewGuid(),
            CustomerName = "Prokhor",
            Date = DateTime.Now.AddDays(-1),
            Products = new List<ProductModel>
            {
                new ProductModel()
                {
                    Id = 4,
                    Name = "Audi RS7",
                    Price = 100000,
                    Category = "Cars",
                    Slug = "audi-rs7"
                }
            },
            TotalPrice = 100000
        },

        new OrderModel
        {
            Id = Guid.NewGuid(),
            CustomerName = "Stas",
            Date = DateTime.Now,
            Products = new List<ProductModel>
            {
                new ProductModel()
                {
                    Id = 5,
                    Name = "Iphone 15",
                    Price = 1000,
                    Category = "Electronics",
                    Slug = "iphone-15"
                },
                new ProductModel()
                {
                Id = 6,
                Name = "desk",
                Price = 50,
                Category = "Furniture",
                Slug = "desk"
            }
            },
            TotalPrice = 1050
        }
    };
    
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(orders);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var order = orders.FirstOrDefault(o => o.Id == id);

        if (order == null)
            return NotFound();

        return Ok(order);
    }
    
    [HttpGet("date/{date:datetime}")]
    public IActionResult GetByDate(DateTime date)
    {
        var result = orders
            .Where(o => o.Date.Date == date.Date)
            .ToList();

        return Ok(result);
    }
    
    [HttpGet("{id:guid}/products")]
    public IActionResult GetOrderProducts(Guid id)
    {
        var order = orders.FirstOrDefault(o => o.Id == id);

        if (order == null)
            return NotFound();

        return Ok(order.Products);
    }
    
    [HttpPost]
    public IActionResult Create(OrderModel order)
    {
        order.Id = Guid.NewGuid();
        orders.Add(order);

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }
    
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, OrderModel updatedOrder)
    {
        var order = orders.FirstOrDefault(o => o.Id == id);

        if (order == null)
            return NotFound();

        order.CustomerName = updatedOrder.CustomerName;
        order.Date = updatedOrder.Date;
        order.Products = updatedOrder.Products;
        order.TotalPrice = updatedOrder.TotalPrice;

        return Ok(order);
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var order = orders.FirstOrDefault(o => o.Id == id);

        if (order == null)
            return NotFound();

        orders.Remove(order);

        return NoContent();
    }
}