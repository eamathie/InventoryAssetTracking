using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Services.Interfaces;

public interface ICategoryService
{
    public Task<Category?> GetByIdAsync(int id);
    public Task<Category?> GetByNameAsync(string categoryName);
    public Task<List<Category>> GetAllAsync();
    public Task<Category> CreateAsync(CategoryDto dto);
    public Task<Category> UpdateAsync(int id, CategoryDto dto);
    public Task DeleteAsync(int id);
    
}