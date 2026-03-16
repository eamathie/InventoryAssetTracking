using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InventoryAssetTracking.Tools;

public class EntityChecker(UserManager<User> userManager, ICategoryRepository categoryRepository, IAssetRepository assetRepository)
{
    public async Task<bool> UserExistsByIdAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        return user != null;
    }

    public async Task<bool> UserExistsByNameAsync(string name)
    {
        var user = await userManager.FindByNameAsync(name);
        return user != null;
    }

    public async Task<bool> CategoryExistsByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        return category != null;
    }

    public async Task<bool> AssetExistsByIdAsync(int id)
    {
        var asset = await assetRepository.GetByIdAsync(id);
        return asset != null;
    }
}