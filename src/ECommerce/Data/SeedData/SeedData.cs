using ECommerce.Dtos;

namespace ECommerce.Data.SeedData
{
    public class SeedData
    {
        public List<CategoryDto> Categories { get; set; } = new();
        public List<ProductDto> Products { get; set; } = new();
    }

}
