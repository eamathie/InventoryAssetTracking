using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Services.Interfaces;

public interface IAssetService
{
    public Task<AssetDto?> GetByIdAsync(int id);
    public Task<AssetDto?> GetByNameAsync(string categoryName);
    public Task<List<AssetDto>> GetAllAsync();
    public Task<AssetDto> CreateAsync(AssetDto dto);
    public Task<AssetDto> UpdateAsync(int id, AssetDto dto);
    public Task DeleteAsync(int id);
}