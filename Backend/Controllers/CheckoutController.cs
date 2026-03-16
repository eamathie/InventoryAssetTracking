using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using InventoryAssetTracking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAssetTracking.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckoutController(ICheckoutService service) : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CheckoutResponseDto>> GetAll()
    {
        var checkouts = await service.GetAllAsync();
        return Ok(checkouts);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CheckoutResponseDto>> GetById(int id)
    {
        var checkout = await service.GetByIdAsync(id);
        if (checkout == null)
            return NotFound();
        
        return Ok(checkout);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CheckoutResponseDto>> Create(CheckoutDto dto)
    {
        try
        {
            var checkout = await service.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetById),
                new { id = checkout.Id },
                checkout);
        }
        catch (InvalidOperationException e)
        {
            return Conflict(new { e.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CheckoutResponseDto>> Update(int id, CheckoutDto dto)
    {
        try
        {
            var updated =  await service.UpdateAsync(id, dto);
            return Ok(updated);
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await  service.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
    }
    
}