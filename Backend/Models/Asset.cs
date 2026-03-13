using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace InventoryAssetTracking.Models;

public class Asset
{
    public enum StatusSet
    {
        Active,
        InRepair,
        Retired
    }
    
    [Key] 
    public int Id { get; set; }
    
    [MaxLength(80)] 
    public required string Name { get; set; }

    public Category Category { get; set; } = null!;
    public int CategoryId { get; set; }
    
    [MaxLength(100)] 
    public string SerialNumber { get; set; } = null!;
    
    public required DateOnly PurchaseDate { get; set; }
    
    public required StatusSet Status  { get; set; }
    
    public User? User { get; set; }
    public string? UserId { get; set; }
    
    [MaxLength(100)]
    public required string QrCodePath { get; set; }
    
    [MaxLength(100)]
    public required string Notes { get; set; }
    
    public required DateTime CreatedAt { get; set; }
    
    public required DateTime UpdatedAt { get; set; }
    
    public ICollection<AssetHistory>? AssetHistory { get; set; }
    
    public ICollection<Checkout>? Checkout { get; set; }
    
    
    
    
}