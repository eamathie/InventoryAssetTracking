using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Services.Interfaces;

public interface IAssetHistoryService
{
    public Task<List<AssetHistoryResponseDto>> GetAllAsync();
    public Task<List<AssetHistoryResponseDto>> GetByAssetIdAsync(int assetId);
    public Task<List<AssetHistoryResponseDto>> GetByDateAsync(DateOnly date);
    public Task<AssetHistoryResponseDto?> GetByIdAsync(int id);
    public Task<AssetHistoryResponseDto> CreateAsync(AssetHistoryDto dto);
    public Task<AssetHistoryResponseDto> UpdateAsync(int id, AssetHistoryDto dto);
    public Task DeleteAsync(int id);
}