using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Text;
using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace InventoryAssetTracking.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(UserManager<User> userManager, IMapper mapper) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(UserRegistrationDto dto)
    {
        if (!dto.Name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            return BadRequest("Name contains invalid characters");

        var user = new User { Name = dto.Name.Trim(), UserName = dto.Email, Email = dto.Email, CreatedAt =  DateTime.UtcNow };
        var result = await userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);
        
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password))
            return Unauthorized($"Invalid email or password");

        var userRoles = await userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
        var token = GenerateJwtToken(authClaims);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddHours(1)
        };

        Response.Cookies.Append("AuthToken", token, cookieOptions);
        return Ok(new{ message = "Auth Token generated successfully"});
    }


    [Authorize]
    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId != id && !User.IsInRole("Admin"))
            return Forbid($"You are not authorized to delete this user");
        
        var user = await userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound($"User not found");
        
        await userManager.DeleteAsync(user);
        return Ok("User deleted successfully");
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(string id, UserRegistrationDto dto)
    {
        var user =  await userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound($"User not found");
        
        user.Email = dto.Email;
        user.UserName = dto.Email;
        user.Name = dto.Name;
        
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var passResult = await userManager.ResetPasswordAsync(user, token, dto.Password);
            
            if (!passResult.Succeeded)
                return BadRequest(passResult.Errors);
        }
        
        return Ok("User updated successfully");
        
    }

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UserResponseDto>>> Get()
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId is null)
            return Unauthorized("User ID claim missing");

        var isAdmin = User.IsInRole("Admin");
        var users = new List<User>();
        if (isAdmin)
            users = await userManager.Users.ToListAsync();
        else
        {
            var user = await userManager.FindByIdAsync(currentUserId);
            if (user is null)
                return NotFound("User not found");
            
            users.Add(user);
        }

        var response = mapper.Map<List<UserResponseDto>>(users);
        return Ok(response);

    }

    [Authorize]
    [HttpGet("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponseDto>> GetById(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound("User not found");
        
        var response = mapper.Map<UserResponseDto>(user);
        return Ok(response);
    }

    [Authorize]
    [HttpGet("roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetCurrentUserRoles()
    {
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUserId is null)
            return Unauthorized("User ID claim missing");
        
        var user = await userManager.FindByIdAsync(currentUserId);
        if (user is null)
            return BadRequest($"Invalid user ID {currentUserId}");
        var roles = await userManager.GetRolesAsync(user);
        return Ok(roles);
    }
    
    private string GenerateJwtToken(List<Claim> claims)
    {
        var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)); // already checked for null in DotEnvLoader
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
            audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: creds
        );

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return token;
    }
}