using System.ComponentModel.DataAnnotations;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.DTOs;

public class AssetResponseDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required int CategoryId { get; set; }
    public required Asset.StatusSet Status { get; set; }
    public required DateOnly PurchaseDate { get; set; }

    [MaxLength(100)]
    public required string QrCodePath { get; set; } 
    public required string UserId { get; set; }
    public required string Notes { get; set; }
}