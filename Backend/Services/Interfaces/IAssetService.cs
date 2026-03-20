using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Services.Interfaces;

public interface IAssetService
{
    public Task<AssetResponseDto?> GetByIdAsync(int id);
    public Task<AssetResponseDto?> GetByNameAsync(string categoryName);
    public Task<List<AssetResponseDto>> GetByUserId(string userId);
    public Task<List<AssetResponseDto>> GetAllAsync();
    public Task<AssetResponseDto> CreateAsync(AssetDto dto);
    public Task<AssetResponseDto> UpdateAsync(int id, AssetDto dto);
    public Task DeleteAsync(int id);
}