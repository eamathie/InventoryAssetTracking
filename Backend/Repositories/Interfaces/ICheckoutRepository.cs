using InventoryAssetTracking.Models;

namespace InventoryAssetTracking.Repositories.Interfaces;

public interface ICheckoutRepository
{
    public Task<List<Checkout>> GetAllAsync();
    public Task<Checkout?> GetByIdAsync(int id);
    public Task CreateAsync(Checkout checkout);
    public Task UpdateAsync(Checkout checkout);
    public Task DeleteAsync(Checkout checkout);
}