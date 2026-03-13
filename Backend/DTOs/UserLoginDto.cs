using System.ComponentModel.DataAnnotations;

namespace InventoryAssetTracking.DTOs;

public class UserLoginDto
{
    [MaxLength(50)]
    public required string Email  { get; set; }
    
    [Length(10, 50)]
    public required string Password { get; set; }
}