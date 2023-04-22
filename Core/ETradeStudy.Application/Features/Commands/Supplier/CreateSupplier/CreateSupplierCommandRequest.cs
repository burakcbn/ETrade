using MediatR;

namespace ETradeStudy.Application.Features.Commands.Supplier.CreateSupplier
{
    public class CreateSupplierCommandRequest:IRequest<bool>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}