using System.ComponentModel.DataAnnotations;

namespace InventoryAssetTracking.Models;

public class AssetHistory
{
    public int Id { get; set; }
    
    public required Asset Asset { get; set; }
    public int AssetId { get; set; }
    
    public User? User { get; set; }
    public string? UserId { get; set; }
    
    [MaxLength(50)]
    public required string Action  { get; set; }
    
    [MaxLength(300)]
    public string? Details  { get; set; }
    
    
    
}