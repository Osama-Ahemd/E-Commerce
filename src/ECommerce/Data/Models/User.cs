namespace ECommerce.Data.Models;

public enum UserRole
{
    Admin,
    Customer
}

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHasher { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }

    public int CartId { get; set; }
    public Cart Cart { get; set; } = null!;

    public List<Review> Reviews { get; set; } = new List<Review>();

    public List<Order> Orders { get; set; } = new List<Order>();
}
