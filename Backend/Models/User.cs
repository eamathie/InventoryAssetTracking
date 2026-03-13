using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace InventoryAssetTracking.Models;

public class User : IdentityUser
{
    [MaxLength(50)]
    public required string Name { get; set; }
    
    public required DateTime CreatedAt { get; set; }
    
    public ICollection<Asset>? Assets { get; set; }
    
    public ICollection<Checkout>? Checkouts { get; set; }
}