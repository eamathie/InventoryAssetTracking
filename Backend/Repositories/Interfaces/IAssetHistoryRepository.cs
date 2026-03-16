using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Repositories.Interfaces;

public interface IAssetHistoryRepository
{
    public Task<List<AssetHistory>> GetAllAsync();
    public Task<List<AssetHistory>> GetByAssetIdAsync(int assetId);
    public Task<List<AssetHistory>> GetByDateAsync(DateOnly date);
    public Task<AssetHistory?> GetByIdAsync(int id);
    public Task CreateAsync(AssetHistory asset);
    public Task UpdateAsync(AssetHistory asset);
    public Task DeleteAsync(AssetHistory asset);
}