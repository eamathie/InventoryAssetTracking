using System.ComponentModel.DataAnnotations;

namespace InventoryAssetTracking.Models;

public class Checkout
{
    public int Id { get; set; }
    
    public int AssetId { get; set; }
    public Asset Asset { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;
    
    public required DateTime CheckedOutAt { get; set; }
    
    public DateTime? CheckedInAt { get; set; }
    
}