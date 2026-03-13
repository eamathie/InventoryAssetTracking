using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Services.Interfaces;

public interface ICheckoutService
{
    public Task<List<Checkout>> GetAllAsync();
    public Task<Checkout?> GetByIdAsync(int id);
    public Task<Checkout> CreateAsync(CheckoutDto dto);
    public Task<Checkout> UpdateAsync(int id, CheckoutDto dto);
    public Task DeleteAsync(int id);
}