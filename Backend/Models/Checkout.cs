using System.ComponentModel.DataAnnotations;

namespace InventoryAssetTracking.Models;

public class Checkout
{
    public int Id { get; set; }
    
    public required Asset Asset { get; set; }
    public int AssetId { get; set; }
    
    public required User User { get; set; }
    public string UserId { get; set; }
    
    public required DateTime CheckedOutAt { get; set; }
    
    public DateTime? CheckedInAt { get; set; }
    
}