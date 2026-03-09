using Microsoft.AspNetCore.Mvc;
using my_aspnet_project.Models;
using my_aspnet_project.DTO;

namespace my_aspnet_project.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private static List<Product> products = new List<Product>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto dto)
        {
            var product = new Product
            {
                Id = products.Count + 1,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CreatedAt = DateTime.UtcNow
            };

            products.Add(product);

            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateProductDto dto)
        {
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);

            if (product == null)
                return NotFound();

            products.Remove(product);

            return NoContent();
        }
    }
}