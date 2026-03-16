using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Services.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAssetTracking.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetHistoryController(IAssetHistoryService service, IMapper mapper) : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AssetHistoryResponseDto>>> Get()
    {
        var assetHistories = await service.GetAllAsync();
        return Ok(assetHistories);
    }

    [Authorize]
    [HttpGet("asset/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AssetHistoryResponseDto>>> GetByAssetId(int id)
    {
        var assetHistories = await service.GetByAssetIdAsync(id);
        return Ok(assetHistories);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AssetHistoryResponseDto>> Get(int id)
    {
        var assetHistory = await service.GetByIdAsync(id);
        if (assetHistory == null)
            return NotFound();
        return Ok(assetHistory);
    }

    [Authorize]
    [HttpGet("by-date")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AssetHistoryResponseDto>>> GetByDate([FromQuery] DateOnly date)
    {
        var assetHistories = await service.GetByDateAsync(date);
        return Ok(assetHistories);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AssetHistoryResponseDto>> Post(AssetHistoryDto dto)
    {
        try
        {
            var assetHistory = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = assetHistory.Id }, assetHistory);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AssetHistoryResponseDto>> Patch(int id, AssetHistoryDto dto)
    {
        try
        {
            var assetHistory = await service.UpdateAsync(id, dto);
            return Ok(assetHistory);
        }
        catch (InvalidOperationException e)
        {
            return BadRequest(e.Message);
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