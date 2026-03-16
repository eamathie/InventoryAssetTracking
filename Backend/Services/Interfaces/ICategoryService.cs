using InventoryAssetTracking.DTOs;

namespace InventoryAssetTracking.Services.Interfaces;

public interface ICategoryService
{
    public Task<CategoryResponseDto?> GetByIdAsync(int id);
    public Task<CategoryResponseDto?> GetByNameAsync(string categoryName);
    public Task<List<CategoryResponseDto>> GetAllAsync();
    public Task<CategoryResponseDto> CreateAsync(CategoryDto dto);
    public Task<CategoryResponseDto> UpdateAsync(int id, CategoryDto dto);
    public Task DeleteAsync(int id);
    
}