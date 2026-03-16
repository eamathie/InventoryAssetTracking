using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Services.Interfaces;

public interface IAssetHistoryService
{
    public Task<List<AssetHistoryDto>> GetAllAsync();
    public Task<List<AssetHistoryDto>> GetByAssetIdAsync(int assetId);
    public Task<List<AssetHistoryDto>> GetByDateAsync(DateOnly date);
    public Task<AssetHistoryDto?> GetByIdAsync(int id);
    public Task<AssetHistoryDto> CreateAsync(AssetHistoryDto dto);
    public Task<AssetHistoryDto> UpdateAsync(int id, AssetHistoryDto dto);
    public Task DeleteAsync(int id);
}