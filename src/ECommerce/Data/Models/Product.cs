namespace ECommerce.Data.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public List<CartItem> CartItems { get; set; } = new List<CartItem>();

    public List<Review> Reviews { get; set; } = new List<Review>();
}