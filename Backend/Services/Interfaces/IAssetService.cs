using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Services.Interfaces;

public interface IAssetService
{
    public Task<Asset?> GetByIdAsync(int id);
    public Task<Asset?> GetByNameAsync(string categoryName);
    public Task<List<Asset>> GetAllAsync();
    public Task<Asset> CreateAsync(AssetDto dto);
    public Task<Asset> UpdateAsync(int id, AssetDto dto);
    public Task DeleteAsync(int id);
}