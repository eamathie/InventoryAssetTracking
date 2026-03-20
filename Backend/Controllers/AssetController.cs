using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using InventoryAssetTracking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAssetTracking.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetController(IAssetService service) : ControllerBase
{

    [Authorize]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AssetResponseDto>> Get(int id)
    {
        var asset = await service.GetByIdAsync(id);
        if (asset == null)
            return NotFound();
        return Ok(asset);
    }

    [Authorize]
    [HttpGet("by-name")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AssetResponseDto>> GetByName(string name)
    {
        var asset = await service.GetByNameAsync(name);
        if (asset == null)
            return NotFound();
        return Ok(asset);
    }

    [Authorize]
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AssetResponseDto>>> GetByUserId(string userId)
    {
        var assets = await service.GetByUserId(userId);
        return Ok(assets);
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AssetResponseDto>>> GetAll()
    {
        var assets = await service.GetAllAsync();
        return Ok(assets);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<AssetResponseDto>> Create(AssetDto dto)
    {
        try
        {
            var asset = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = asset.Id }, asset);

        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AssetResponseDto>> Update(int id, AssetDto dto)
    {
        try
        {
            var asset = await service.UpdateAsync(id, dto);
            return Ok(asset);
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
    }
}