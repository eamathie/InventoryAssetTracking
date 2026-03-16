using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Services.Interfaces;

public interface ICheckoutService
{
    public Task<List<CheckoutDto>> GetAllAsync();
    public Task<CheckoutDto?> GetByIdAsync(int id);
    public Task<CheckoutDto> CreateAsync(CheckoutDto dto);
    public Task<CheckoutDto> UpdateAsync(int id, CheckoutDto dto);
    public Task DeleteAsync(int id);
}