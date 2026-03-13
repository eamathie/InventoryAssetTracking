namespace InventoryAssetTracking.DTOs;

public class CheckoutDto
{
    public required string UserId { get; set; }
    public required int AssetId { get; set; }
    public required DateTime CheckedOutAt { get; set; }
    public DateTime? CheckedInAt { get; set; }
}