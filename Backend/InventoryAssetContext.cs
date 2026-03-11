using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace InventoryAssetTracking;

public class InventoryAssetContext : IdentityDbContext<IdentityUser>
{
    
}