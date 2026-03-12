using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InventoryAssetTracking.Models;

[Index(nameof(Name),  IsUnique = true)]
public class Category
{
    public int Id { get; set; }
    [MaxLength(50)] public required string Name { get; set; }
    
    public ICollection<Asset>? Assets { get; set; }
}