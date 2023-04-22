using MediatR;

namespace ETradeStudy.Application.Features.Queries.Supplier.GetAllSupplier
{
    public class GetAllSupplierQueryRequest:IRequest<List<GetAllSupplierQueryResponse>>
    {
    }
}