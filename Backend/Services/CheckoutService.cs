using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using InventoryAssetTracking.Repositories.Interfaces;
using InventoryAssetTracking.Services.Interfaces;

namespace InventoryAssetTracking.Services;

public class CheckoutService(ICheckoutRepository repository) : ICheckoutService
{
    public async Task<List<Checkout>> GetAllAsync()
    {
        var checkouts = await repository.GetAllAsync();
        return checkouts;
    }

    public async Task<Checkout?> GetByIdAsync(int id)
    {
        var checkout = await repository.GetByIdAsync(id);
        return checkout;
    }

    public async Task<Checkout> CreateAsync(CheckoutDto dto)
    {
        var checkouts = await repository.GetAllAsync();
        var checkout = checkouts.First(c => c.AssetId == dto.AssetId && c.CheckedOutAt == dto.CheckedOutAt);
        if (checkout != null)
            throw new InvalidOperationException($"Checkout for asset with id {checkout.AssetId} checked out at {checkout.CheckedOutAt} already exists");
        
        checkout = new Checkout
        {
            UserId = dto.UserId,
            AssetId = dto.AssetId,
            CheckedOutAt = dto.CheckedOutAt,
        };
        
        await repository.CreateAsync(checkout);
        return checkout;
    }

    public async Task<Checkout> UpdateAsync(int id, CheckoutDto dto)
    {
        var checkout = await repository.GetByIdAsync(id);
        if (checkout == null)
            throw new  InvalidOperationException($"Checkout for asset with id {id} does not exist");
        
        checkout.UserId = dto.UserId;
        checkout.AssetId = dto.AssetId;
        checkout.CheckedOutAt = dto.CheckedOutAt;
        checkout.CheckedInAt = dto.CheckedInAt;
        
        await repository.UpdateAsync(checkout);
        return checkout;
    }

    public async Task DeleteAsync(int id)
    {
        var checkout = await repository.GetByIdAsync(id);
        if (checkout == null)
            throw new  InvalidOperationException($"Checkout for asset with id {id} does not exist");
        
        await repository.DeleteAsync(checkout);
    }
}