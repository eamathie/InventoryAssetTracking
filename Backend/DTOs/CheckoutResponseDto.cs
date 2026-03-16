namespace InventoryAssetTracking.DTOs;

public class CheckoutResponseDto
{
    public required int Id { get; set; }
    public required string UserId { get; set; }
    public required int AssetId { get; set; }
    public required DateTime CheckedOutAt { get; set; }
    public DateTime? CheckedInAt { get; set; }
}