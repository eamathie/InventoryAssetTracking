using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryAssetTracking.Repositories;

public class AssetHistoryRepository(InventoryAssetContext context) : IAssetHistoryRepository
{
    public async Task<List<AssetHistory>> GetAllAsync()
    {
        return await context.AssetHistories.ToListAsync();
    }

    public async Task<List<AssetHistory>> GetByDateAsync(DateOnly date)
    {
        return await context.AssetHistories.Where(a => DateOnly.FromDateTime(a.CreatedAt) == date).ToListAsync();
    }
    
    public async Task<List<AssetHistory>> GetByAssetIdAsync(int assetId)
    {
        return await context.AssetHistories.Where(x => x.AssetId == assetId).ToListAsync();
    }

    public async Task<AssetHistory?> GetByIdAsync(int id)
    {
        return await context.AssetHistories.FindAsync(id);
    }


    public async Task CreateAsync(AssetHistory asset)
    {
        context.AssetHistories.Add(asset);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AssetHistory asset)
    {
        context.AssetHistories.Update(asset);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AssetHistory asset)
    {
        context.AssetHistories.Remove(asset);
        await context.SaveChangesAsync();
    }
}