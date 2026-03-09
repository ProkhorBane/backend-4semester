using System.ComponentModel.DataAnnotations;
namespace my_aspnet_project.DTO;
public class CreateProductDto
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [Range(1, 100000000)]
    public decimal Price { get; set; }
}