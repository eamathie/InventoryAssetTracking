using System.Runtime.InteropServices.JavaScript;
using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using InventoryAssetTracking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAssetTracking.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(ICategoryService service) : ControllerBase
{
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    public async Task<ActionResult<CategoryResponseDto>> GetAll()
    {
        var categories = await service.GetAllAsync();
        return Ok(categories);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryResponseDto>> GetById(int id)
    {
        var category = await service.GetByIdAsync(id);
        if (category == null)
            return NotFound();
        
        return Ok(category);
    }

    [Authorize]
    [HttpGet("name/{categoryName}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryResponseDto>> GetByName(string categoryName)
    {
        var category =  await service.GetByNameAsync(categoryName);
        if (category == null)
            return NotFound();
        
        return  Ok(category);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(Category), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CategoryResponseDto>> Create(CategoryDto categoryDto)
    {
        try
        {
            var category = await service.CreateAsync(categoryDto);
            return CreatedAtAction(
                nameof(GetByName),
                new { categoryName = category.Name },
                category
            );
        }
        catch (InvalidOperationException e)
        {
            return Conflict(new { message = e.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id:int}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryResponseDto>> Update(int id, CategoryDto categoryDto)
    {
        try
        {
            var updated = await service.UpdateAsync(id, categoryDto);
            return Ok(updated);
        }
        catch (InvalidOperationException e)
        {
            return NotFound(e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryResponseDto>> Delete(int id)
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