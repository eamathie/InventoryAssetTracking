using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Services.Interfaces;

public interface ICategoryService
{
    public Task<CategoryDto?> GetByIdAsync(int id);
    public Task<CategoryDto?> GetByNameAsync(string categoryName);
    public Task<List<CategoryDto>> GetAllAsync();
    public Task<CategoryDto> CreateAsync(CategoryDto dto);
    public Task<CategoryDto> UpdateAsync(int id, CategoryDto dto);
    public Task DeleteAsync(int id);
    
}