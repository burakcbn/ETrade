using MediatR;

namespace ETradeStudy.Application.Features.Commands.Product.ProductUpdateWithQRCode
{
    public class ProductUpdateWithQRCodeCommandRequest:IRequest<ProductUpdateWithQRCodeCommandResponse>
    {
        public string ProductId { get; set; }
        public int Stock { get; set; }
    }
}