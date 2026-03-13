using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.DTOs;

public class AssetDto
{
    public required string Name { get; set; }
    public required int CategoryId { get; set; }
    public required Asset.StatusSet Status { get; set; }
    public required DateOnly PurchaseDate { get; set; }
    public required string UserId { get; set; }
    public required string Notes { get; set; }
    
}