using System.ComponentModel.DataAnnotations;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.DTOs;

public class AssetHistoryDto
{
    public int AssetId { get; set; }
    public string? UserId { get; set; }
    
    [MaxLength(50)]
    public required string Action  { get; set; }
    
    [MaxLength(300)]
    public string? Details  { get; set; }
    
    public DateTime CreatedAt { get; set; }
}