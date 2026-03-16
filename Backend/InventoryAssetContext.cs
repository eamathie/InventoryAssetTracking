using InventoryAssetTracking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InventoryAssetTracking;

public class InventoryAssetContext(DbContextOptions<InventoryAssetContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<AssetHistory> AssetHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();
        
        builder.Entity<Asset>()
            .Property(a => a.Status)
            .HasConversion<string>();

        builder.Entity<Asset>()
            .HasIndex(a => a.SerialNumber)
            .IsUnique();

        builder.Entity<Asset>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();
        
        builder.Entity<Asset>()
            .Property(a => a.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

        builder.Entity<Checkout>()
            .Property(c => c.CheckedOutAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

        builder.Entity<AssetHistory>()
            .Property(h => h.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .ValueGeneratedOnAdd();

    }
}