using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Repositories.Interfaces;

public interface IAssetRepository
{
    public Task<List<Asset>> GetAllAsync();
    public Task<Asset?> GetByIdAsync(int id);
    public Task<Asset?> GetByNameAsync(string assetName);
    public Task<List<Asset>> GetByUserId(string id);
    public Task CreateAsync(Asset asset);
    public Task UpdateAsync(Asset asset);
    public Task DeleteAsync(Asset asset);
}