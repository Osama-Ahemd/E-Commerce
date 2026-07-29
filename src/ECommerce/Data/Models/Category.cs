namespace ECommerce.Data.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int? ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; } = null!;

    public List<Product> Products { get; set; } = new List<Product>();
}