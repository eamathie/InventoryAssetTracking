using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Repositories.Interfaces;

public interface ICategoryRepository
{
    public Task<List<Category>> GetAllAsync();
    public Task<Category?> GetByIdAsync(int id);
    public Task<Category?> GetByNameAsync(string categoryName);
    public Task CreateAsync(Category category);
    public Task UpdateAsync(Category category);
    public Task DeleteAsync(Category category);
}