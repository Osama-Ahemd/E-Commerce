using ECommerce.Data;
using ECommerce.Data.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ECommerce.Data.SeedData;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context, string jsonPath)
    {
        if (await context.Categories.AnyAsync()) return;

        var json = await File.ReadAllTextAsync(jsonPath);
        var data = JsonSerializer.Deserialize<SeedData>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        var idMap = new Dictionary<int, Category>();

        Category Resolve(int jsonId)
        {
            if (idMap.TryGetValue(jsonId, out var existing)) return existing;

            var dto = data.Categories.First(c => c.Id == jsonId);
            var entity = new Category { Name = dto.Name };

            if (dto.ParentCategoryId.HasValue)
                entity.ParentCategory = Resolve(dto.ParentCategoryId.Value);

            idMap[jsonId] = entity;
            context.Categories.Add(entity);
            return entity;
        }

        foreach (var c in data.Categories)
            Resolve(c.Id);

        foreach (var p in data.Products)
        {
            context.Products.Add(new Product
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                Category = idMap[p.CategoryId]
            });
        }

        await context.SaveChangesAsync();
    }
}