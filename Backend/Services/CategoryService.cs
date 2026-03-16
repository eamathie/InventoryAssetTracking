using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories.Interfaces;
using InventoryAssetTracking.Services.Interfaces;
using MapsterMapper;

namespace InventoryAssetTracking.Services;

public class CategoryService(ICategoryRepository repository, IMapper mapper) : ICategoryService
{
    public async Task<CategoryResponseDto?> GetByIdAsync(int id)
    {
        var category = await repository.GetByIdAsync(id);
        return category == null ? null : mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<CategoryResponseDto?> GetByNameAsync(string categoryName)
    {
        var category = await repository.GetByNameAsync(categoryName);
        return category == null ? null : mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var categories =  await repository.GetAllAsync();
        return mapper.Map<List<CategoryResponseDto>>(categories);
    }

    public async Task<CategoryResponseDto> CreateAsync(CategoryDto dto)
    {
        var category = await repository.GetByNameAsync(dto.Name);
        if (category != null)
            throw new InvalidOperationException($"Category with name {dto.Name} already exists");
        
        category = new Category { Name =  dto.Name };
        
        await  repository.CreateAsync(category);
        return mapper.Map<CategoryResponseDto>(category);
    }

    public async Task<CategoryResponseDto> UpdateAsync(int id, CategoryDto dto)
    {
        var category = await repository.GetByIdAsync(id);
        if (category == null)
            throw new InvalidOperationException($"Category with id {id} not found");
        
        category.Name = dto.Name;
        await repository.UpdateAsync(category);
        
        return mapper.Map<CategoryResponseDto>(category);
    }

    public async Task DeleteAsync(int id)
    {
        var category = await repository.GetByIdAsync(id);
        if (category == null)
            throw new InvalidOperationException($"Category with id {id} not found");
        
        await repository.DeleteAsync(category);
    }
}