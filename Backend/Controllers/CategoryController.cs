using System.Runtime.InteropServices.JavaScript;
using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryAssetTracking.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(InventoryAssetContext context) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    public async Task<ActionResult<Category>> Get()
    {
        var categories = await context.Categories.ToListAsync();
        return Ok(categories);
    }

    [HttpGet("{categoryName}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Category>> GetByName(string categoryName)
    {
        var category = await context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
        
        if (category == null)
            return NotFound();
        
        return  Ok(category);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Category), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Category>> Create(CategoryDto categoryDto)
    {
        var exists  = await context.Categories
            .AnyAsync(c => c.Name.ToLower().Trim()
                .Equals(categoryDto.Name.ToLower().Trim()));
        
        if (exists)
            return Conflict(new { message =  "Category already exists." });
        
        var category = new Category{ Name =  categoryDto.Name.Trim() };
        
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetByName), new  { categoryName = category.Name }, category);
    }
}