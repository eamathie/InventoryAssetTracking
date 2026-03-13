using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace InventoryAssetTracking.DTOs;

public class UserRegistrationDto
{
    [MaxLength(50)]
    public required string Email  { get; set; }
    
    [MaxLength(50)]
    public required string Name { get; set; }
    
    [Length(10, 50)]
    public required string Password { get; set; }
    
    
}