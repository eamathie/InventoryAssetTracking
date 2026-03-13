using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryAssetTracking.Repositories;

public class CheckoutRepository(InventoryAssetContext context) : ICheckoutRepository
{
    public async Task<List<Checkout>> GetAllAsync()
    {
        return await context.Checkouts.ToListAsync();
    }

    public async Task<Checkout?> GetByIdAsync(int id)
    {
        return await context.Checkouts.FindAsync(id);
    }

    public async Task CreateAsync(Checkout checkout)
    {
        context.Checkouts.Add(checkout);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Checkout checkout)
    {
        context.Checkouts.Update(checkout);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Checkout checkout)
    {
        context.Checkouts.Remove(checkout);
        await context.SaveChangesAsync();
    }
}