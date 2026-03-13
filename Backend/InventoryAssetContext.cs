using InventoryAssetTracking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryAssetTracking;

public class InventoryAssetContext: IdentityDbContext<User>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<AssetHistory> AssetHistories { get; set; }
    
    public InventoryAssetContext(DbContextOptions<InventoryAssetContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Entity<Asset>()
            .Property(a => a.Status)
            .HasConversion<string>();

        builder.Entity<Asset>()
            .HasIndex(a => a.SerialNumber)
            .IsUnique();

        builder.Entity<Asset>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Entity<Asset>()
            .Property(a => a.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Entity<Checkout>()
            .Property(c => c.CheckedOutAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
    }
}