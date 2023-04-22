using AutoMapper;
using ETradeStudy.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Commands.Supplier.CreateSupplier
{
    public class CreateSupplierCommandHandler : IRequestHandler<CreateSupplierCommandRequest, bool>
    {
        private readonly ISupplierWrite supplierWrite;
        private readonly IMapper mapper;

        public CreateSupplierCommandHandler(ISupplierWrite supplierWrite, IMapper mapper)
        {
            this.supplierWrite = supplierWrite;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(CreateSupplierCommandRequest request, CancellationToken cancellationToken)
        {
            if(request == null) { throw new Exception("Alanları doldurunuz"); }
            var supplier= mapper.Map<Domain.Entities.Supplier>(request);
           var succeeded= await supplierWrite.AddAsync(supplier);
            await supplierWrite.SaveAsync();
            return succeeded;
        }
    }
}
