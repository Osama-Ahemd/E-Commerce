namespace ECommerce.Data.Models;

public class Cart
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public List<CartItem> CartItems { get; set; } = new List<CartItem>();
}

