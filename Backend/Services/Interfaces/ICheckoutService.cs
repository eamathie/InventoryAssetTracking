using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Services.Interfaces;

public interface ICheckoutService
{
    public Task<List<CheckoutResponseDto>> GetAllAsync();
    public Task<CheckoutResponseDto?> GetByIdAsync(int id);
    public Task<CheckoutResponseDto> CreateAsync(CheckoutDto dto);
    public Task<CheckoutResponseDto> UpdateAsync(int id, CheckoutDto dto);
    public Task DeleteAsync(int id);
}