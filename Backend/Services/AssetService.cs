using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories.Interfaces;
using InventoryAssetTracking.Services.Interfaces;
using InventoryAssetTracking.Tools;

namespace InventoryAssetTracking.Services;

public class AssetService(IAssetRepository repository, AssetQrGenerator assetQrGenerator) : IAssetService
{
    public async Task<Asset?> GetByIdAsync(int id)
    {
        var asset = await repository.GetByIdAsync(id);
        return asset;
    }

    public async Task<Asset?> GetByNameAsync(string categoryName)
    {
        var asset = await repository.GetByNameAsync(categoryName);
        return asset;
    }

    public async Task<List<Asset>> GetAllAsync()
    {
        var assets = await repository.GetAllAsync();
        return assets;
    }

    public async Task<Asset> CreateAsync(AssetDto dto)
    {
        var asset = new Asset
        {
            Name = dto.Name,
            UserId = dto.UserId,
            CategoryId = dto.CategoryId,
            PurchaseDate = dto.PurchaseDate,
            Status = Asset.StatusSet.Active,
            QrCodePath = null,
            Notes = dto.Notes,
            CreatedAt = default,
            UpdatedAt = default,
        };
        await repository.CreateAsync(asset);
        
        var qrCodePath = await assetQrGenerator.GenerateAssetQrCode(asset.Id);
        
        asset.QrCodePath = qrCodePath;
        await repository.UpdateAsync(asset);
        
        return asset;
    }

    public async Task<Asset> UpdateAsync(int id, AssetDto dto)
    {
        var asset = await repository.GetByIdAsync(id);
        if (asset == null)
            throw new InvalidOperationException($"Asset with id {id} not found");
        
        asset.Name = dto.Name;
        asset.UserId = dto.UserId;
        asset.CategoryId = dto.CategoryId;
        asset.PurchaseDate = dto.PurchaseDate;
        asset.Status = Asset.StatusSet.Active;
        asset.Notes = dto.Notes;
        asset.UpdatedAt = DateTime.UtcNow;
        
        await repository.UpdateAsync(asset);
        return asset;
    }

    public async Task DeleteAsync(int id)
    {
        var asset  = await repository.GetByIdAsync(id);
        if (asset == null)
            throw new InvalidOperationException($"Asset with id {id} not found");
        
        await repository.DeleteAsync(asset);
    }
}