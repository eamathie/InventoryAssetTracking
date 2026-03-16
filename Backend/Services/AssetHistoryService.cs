using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories.Interfaces;
using InventoryAssetTracking.Services.Interfaces;
using InventoryAssetTracking.Tools;

namespace InventoryAssetTracking.Services;

public class AssetHistoryService(IAssetHistoryRepository repository, EntityChecker entityChecker) : IAssetHistoryService
{
    public async Task<List<AssetHistory>> GetAllAsync()
    {
        var assetHistories = await repository.GetAllAsync();
        return assetHistories;
    }

    public async Task<List<AssetHistory>> GetByAssetIdAsync(int assetId)
    {
        var assetHistories = await repository.GetByAssetIdAsync(assetId);
        return assetHistories;
    }

    public async Task<AssetHistory?> GetByIdAsync(int id)
    {
        var assetHistory = await repository.GetByIdAsync(id);
        return assetHistory;
    }

    public async Task<List<AssetHistory>> GetByDateAsync(DateOnly date)
    {
        var assetHistories =  await repository.GetByDateAsync(date);
        return assetHistories;
    }

    public async Task<AssetHistory> CreateAsync(AssetHistoryDto dto)
    {
        if (dto.UserId != null && !await entityChecker.UserExistsByIdAsync(dto.UserId))
            throw new InvalidOperationException($"User {dto.UserId} does not exist");
        if (!await entityChecker.AssetExistsByIdAsync(dto.AssetId))
            throw new InvalidOperationException($"Asset History with ID {dto.AssetId} not found");
            
        var assetHistory = new AssetHistory
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            Action = dto.Action,
            Details = dto.Details,
        };
        
        await repository.CreateAsync(assetHistory);
        return assetHistory;
    }

    public async Task<AssetHistory> UpdateAsync(int id, AssetHistoryDto dto)
    {
        var assetHistory = await repository.GetByIdAsync(id);
        if (assetHistory == null)
            throw new InvalidOperationException($"AssetHistory with ID {id} not found");
        if (assetHistory.UserId != null && !await entityChecker.UserExistsByIdAsync(assetHistory.UserId))
            throw new InvalidOperationException($"User {assetHistory.UserId} does not exist");
        if (!await entityChecker.AssetExistsByIdAsync(dto.AssetId))
            throw new InvalidOperationException($"Asset History with ID {dto.AssetId} not found");
        
        assetHistory.UserId = dto.UserId;
        assetHistory.AssetId = dto.AssetId;
        assetHistory.Action = dto.Action;
        assetHistory.Details = dto.Details;
        assetHistory.CreatedAt = dto.CreatedAt;
        
        await repository.UpdateAsync(assetHistory);
        return assetHistory;
    }

    public async Task DeleteAsync(int id)
    {
        var assetHistory = await repository.GetByIdAsync(id);
        if (assetHistory == null)
            throw new InvalidOperationException($"AssetHistory with ID {id} not found");
        
        await repository.DeleteAsync(assetHistory);
    }
}