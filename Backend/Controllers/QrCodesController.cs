using InventoryAssetTracking.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAssetTracking.Controllers;

[ApiController]
[Route("[controller]")]
public class QrCodeController(AssetQrGenerator assetQrGenerator) : ControllerBase
{
    [Authorize]
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByPath(int id)
    {
        var qrImage = await assetQrGenerator.GetQrCode(id);
        if (qrImage == null)
            return NotFound("QR code not found");
        
        return Ok(qrImage);
    }
}
