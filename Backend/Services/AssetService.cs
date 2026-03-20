using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories.Interfaces;
using InventoryAssetTracking.Services.Interfaces;
using InventoryAssetTracking.Tools;
using MapsterMapper;

namespace InventoryAssetTracking.Services;

public class AssetService(IAssetRepository repository, AssetQrGenerator assetQrGenerator, EntityChecker entityChecker, IMapper mapper) : IAssetService
{
    public async Task<AssetResponseDto?> GetByIdAsync(int id)
    {
        var asset = await repository.GetByIdAsync(id);
        return asset == null ? null : mapper.Map<AssetResponseDto>(asset);
    }

    public async Task<AssetResponseDto?> GetByNameAsync(string assetName)
    {
        var asset = await repository.GetByNameAsync(assetName);
        return asset == null ? null : mapper.Map<AssetResponseDto>(asset);
    }

    public async Task<List<AssetResponseDto>> GetByUserId(string userId)
    {
        var assets = await repository.GetByUserId(userId);
        return mapper.Map<List<AssetResponseDto>>(assets);
    }

    public async Task<List<AssetResponseDto>> GetAllAsync()
    {
        var assets = await repository.GetAllAsync();
        return mapper.Map<List<AssetResponseDto>>(assets);
    }

    public async Task<AssetResponseDto> CreateAsync(AssetDto dto)
    {
        if (!await entityChecker.UserExistsByIdAsync(dto.UserId))
            throw new InvalidOperationException($"User with id {dto.UserId} not found");
        if (!await entityChecker.CategoryExistsByIdAsync(dto.CategoryId))
            throw new InvalidOperationException($"Category with id {dto.CategoryId} not found");
        
        var asset = new Asset
        {
            Name = dto.Name,
            UserId = dto.UserId,
            CategoryId = dto.CategoryId,
            PurchaseDate = dto.PurchaseDate,
            Status = Asset.StatusSet.Active,
            QrCodePath = null, // set below
            Notes = dto.Notes,
            CreatedAt = default,
            UpdatedAt = default,
            SerialNumber = await GenerateSerialNumAsync()
        };
        await repository.CreateAsync(asset);
        
        // generate QR code and update Asset
        var qrCodePath = await assetQrGenerator.GenerateAssetQrCode(asset.Id);
        asset.QrCodePath = qrCodePath;
        
        await repository.UpdateAsync(asset);
        return mapper.Map<AssetResponseDto>(asset);
    }

    public async Task<AssetResponseDto> UpdateAsync(int id, AssetDto dto)
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
        return mapper.Map<AssetResponseDto>(asset);
    }

    public async Task DeleteAsync(int id)
    {
        var asset  = await repository.GetByIdAsync(id);
        if (asset == null)
            throw new InvalidOperationException($"Asset with id {id} not found");
        
        await repository.DeleteAsync(asset);
    }
    
    /// <summary>
    /// Helper method that generates unique serial numbers for assets
    /// </summary>
    /// <returns></returns>
    private async Task<string> GenerateSerialNumAsync()
    {
        var last = await repository.GetAllAsync();
        var lastSerialNum = last.OrderByDescending(a => a.Id).Select(a => a.SerialNumber).FirstOrDefault();
        
        var nextNumber = lastSerialNum == null
            ? 1
            : int.Parse(lastSerialNum.Split('-')[1]) + 1;

        return $"ASSET-{nextNumber:D4}"; // 4-digits, ex: ASSET-0001
    }
}