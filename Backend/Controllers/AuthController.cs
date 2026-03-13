using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Text;
using InventoryAssetTracking.DTOs;
using InventoryAssetTracking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InventoryAssetTracking.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(UserManager<User> userManager, IConfiguration config) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(JSType.Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(UserRegistrationDto dto)
    {
        if (!dto.Name.All(char.IsLetter))
            return BadRequest("Name contains invalid characters");

        var user = new User { UserName = dto.Name, Email = dto.Email, CreatedAt =  DateTime.Now };
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

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        var token = GenerateJwtToken(authClaims);
        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo
        });
    }
    

    private JwtSecurityToken GenerateJwtToken(List<Claim> claims)
    {
        var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!)); // already checked for null in DotEnvLoader
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
            audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: creds
        );

    }
}