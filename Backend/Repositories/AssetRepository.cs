using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryAssetTracking.Repositories;

public class AssetRepository(InventoryAssetContext context) : IAssetRepository
{
    public async Task<List<Asset>> GetAllAsync()
    {
        return await context.Assets.ToListAsync();
    }

    public async Task<Asset?> GetByIdAsync(int id)
    {
        return await context.Assets.FindAsync(id);
    }

    public async Task<Asset?> GetByNameAsync(string assetName)
    {
        return await context.Assets.FirstOrDefaultAsync(a => 
            a.Name.ToLower().Trim()
            .Equals(assetName.ToLower().Trim()));
    }

    public async Task CreateAsync(Asset asset)
    {
        context.Assets.Add(asset);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Asset asset)
    {
        context.Assets.Update(asset);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Asset asset)
    {
        context.Assets.Remove(asset);
        await context.SaveChangesAsync();
    }
}