using QRCoder;

namespace AdventQRlendar.Services;

public sealed class QRCodeService
{
    public byte[] GetQRCode(string url, int pixelsPerModule)
    {
        using QRCodeGenerator generator = new();
        using QRCodeData data = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.L);

        using PngByteQRCode qrCode = new(data);
        byte[] graphic = qrCode.GetGraphic(pixelsPerModule);

        return graphic;
    }
}
