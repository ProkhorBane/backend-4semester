namespace my_aspnet_project.Models;

public class OrderModel
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = "";
    public DateTime Date { get; set; }
    public List<ProductModel> Products { get; set; } = new();
    public decimal TotalPrice { get; set; }
}