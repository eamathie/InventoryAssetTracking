using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Services.Interfaces;

public interface IAssetHistoryService
{
    public Task<List<AssetHistory>> GetAllAsync();
    public Task<List<AssetHistory>> GetByAssetIdAsync(int assetId);
    public Task<List<AssetHistory>> GetByDateAsync(DateOnly date);
    public Task<AssetHistory?> GetByIdAsync(int id);
    public Task<AssetHistory> CreateAsync(AssetHistoryDto dto);
    public Task<AssetHistory> UpdateAsync(int id, AssetHistoryDto dto);
    public Task DeleteAsync(int id);
}