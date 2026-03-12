using InventoryAssetTracking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryAssetTracking;

public class InventoryAssetContext: IdentityDbContext<IdentityUser>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public InventoryAssetContext(DbContextOptions<InventoryAssetContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Asset>()
            .Property(a => a.Status)
            .HasConversion<string>();

        builder.Entity<Asset>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("getdate()");
        
        builder.Entity<Asset>()
            .Property(a => a.UpdatedAt)
            .HasDefaultValueSql("getdate()");
        
        
    }
}