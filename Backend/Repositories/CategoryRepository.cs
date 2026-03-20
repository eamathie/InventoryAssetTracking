using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryAssetTracking.Repositories;

public class CategoryRepository(InventoryAssetContext context) : ICategoryRepository
{
    public async Task<List<Category>> GetAllAsync()
    {
        return await context.Categories.Include(c => c.Assets).ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await context.Categories.Include(c => c.Assets).Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Category?> GetByNameAsync(string categoryName)
    {
        return await context.Categories.FirstOrDefaultAsync(c => 
            c.Name.ToLower().Trim()
            .Equals(categoryName.ToLower().Trim()));
    }

    public async Task CreateAsync(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }
}