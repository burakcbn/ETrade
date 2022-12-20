using ETradeStudy.Application.Abstractions.Services;
using QRCoder;

namespace ETradeStudy.Infrastructure.Services
{
    public class QRCodeService : IQRCodeService
    {
        public QRCodeService()
        {
            
        }

        public byte[] GenerateQRCode(string plainText)
        {
            QRCodeGenerator qRCode = new();
            QRCodeData data = qRCode.CreateQrCode(plainText, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(data);
            byte[] byteGraphic= qrCode.GetGraphic(250, new byte[] { 84, 99, 71 }, new byte[] { 240, 240, 240 });
            return byteGraphic;
        }
    }
}
