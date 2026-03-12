using Microsoft.AspNetCore.Identity;

namespace InventoryAssetTracking.Models;

public class User : IdentityUser
{
    public required DateTime CreatedAt { get; set; }
    
    public ICollection<Asset>? Assets { get; set; }
    
    public ICollection<Checkout>? Checkouts { get; set; }
}