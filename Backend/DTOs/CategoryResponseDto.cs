using System.ComponentModel.DataAnnotations;

namespace InventoryAssetTracking.DTOs;

public class CategoryResponseDto
{
    public int Id { get; set; }
    [MaxLength(50)] public required string Name { get; set; }
    
    public ICollection<AssetResponseDto>? Assets { get; set; }
}