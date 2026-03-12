using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories.Interfaces;
using InventoryAssetTracking.Services.Interfaces;

namespace InventoryAssetTracking.Services;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public async Task<Category?> GetByIdAsync(int id)
    {
        var category = await repository.GetByIdAsync(id);
        return category;
    }

    public async Task<Category?> GetByNameAsync(string categoryName)
    {
        var category = await repository.GetByNameAsync(categoryName);
        return category;
    }

    public async Task<List<Category>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<Category> CreateAsync(CategoryDto dto)
    {
        var category = await repository.GetByNameAsync(dto.Name);
        if (category != null)
            throw new InvalidOperationException($"Category with name {dto.Name} already exists");
        
        category = new Category { Name =  dto.Name };
        
        await  repository.CreateAsync(category);
        return category;
    }

    public async Task<Category> UpdateAsync(int id, CategoryDto dto)
    {
        var category = await repository.GetByIdAsync(id);
        if (category == null)
            throw new InvalidOperationException($"Category with id {id} not found");
        
        category.Name = dto.Name;
        await repository.UpdateAsync(category);
        
        return category;
    }

    public async Task DeleteAsync(int id)
    {
        var category = await repository.GetByIdAsync(id);
        if (category == null)
            throw new InvalidOperationException($"Category with id {id} not found");
        
        await repository.DeleteAsync(category);
    }
}