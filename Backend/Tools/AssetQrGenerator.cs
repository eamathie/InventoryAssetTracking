using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace InventoryAssetTracking.Tools;

public class AssetQrGenerator(IWebHostEnvironment environment, ILogger<AssetQrGenerator> logger)
{
    private readonly QRCodeGenerator _generator = new();
    
    public async Task<string> GenerateAssetQrCode(int id)
    {
        var qrCodeData = _generator.CreateQrCode(id.ToString(), QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrCodeData);
        var pngBytes = qrCode.GetGraphic(20);
        
        var qrFolder = Path.Combine(environment.WebRootPath, "QrCodes");
        Directory.CreateDirectory(qrFolder);
        var filePath = Path.Combine(qrFolder, $"{id}.png");
        
        await File.WriteAllBytesAsync(filePath, pngBytes);
        
        return  $"/QrCodes/{id}.png";
    }

    public async Task<FileContentResult?> GetQrCode(int id)
    {
        var qrFolder = Path.Combine(environment.WebRootPath, $"QrCodes/{id}.png");
        logger.LogInformation(qrFolder);
        if (!File.Exists(qrFolder))
            return null;
        var bytes = await File.ReadAllBytesAsync(qrFolder);
        return new FileContentResult(bytes, "image/png");//Convert.ToBase64String(bytes);
    }
}