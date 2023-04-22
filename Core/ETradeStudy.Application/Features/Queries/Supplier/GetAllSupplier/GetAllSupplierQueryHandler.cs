using AutoMapper;
using ETradeStudy.Application.Repositories;
using ETradeStudy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Queries.Supplier.GetAllSupplier
{
    public class GetAllSupplierQueryHandler : IRequestHandler<GetAllSupplierQueryRequest, List<GetAllSupplierQueryResponse>>
    {
        private readonly ISupplierRead supplierRead;
        private readonly IMapper mapper;

        public GetAllSupplierQueryHandler(ISupplierRead supplierRead, IMapper mapper)
        {
            this.supplierRead = supplierRead;
            this.mapper = mapper;
        }

        public  async Task<List<GetAllSupplierQueryResponse>>Handle(GetAllSupplierQueryRequest request, CancellationToken cancellationToken)
        {
            var suppliers = supplierRead.GetAll(false);
            var mapSuppliers= mapper.Map<List<Domain.Entities.Supplier>, List<GetAllSupplierQueryResponse>>(await suppliers.ToListAsync());
            return mapSuppliers;
        }
    }
}
