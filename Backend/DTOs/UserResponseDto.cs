using System.ComponentModel.DataAnnotations;

namespace InventoryAssetTracking.DTOs;

public class UserResponseDto
{
    public required string Id { get; set; }
    
    [MaxLength(50)]
    public required string Name { get; set; }
    
    public required DateTime CreatedAt { get; set; }
    
    public ICollection<AssetResponseDto>? Assets { get; set; }
    
    public ICollection<CheckoutResponseDto>? Checkouts { get; set; }
}