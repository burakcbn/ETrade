using AutoMapper;
using ETradeStudy.Application.DTOs.Product;
using ETradeStudy.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Application.Features.Queries.Product.GeyByDynamic
{
    public class GetByDynamicQueryHandler : IRequestHandler<GetByDynamicQueryRequest, GetByDynamicQueryResponse>
    {
        private readonly IProductRead _productRead;
        private readonly IMapper _mapper;

        public GetByDynamicQueryHandler(IProductRead productRead, IMapper mapper = null)
        {
            _productRead = productRead;
            _mapper = mapper;
        }

        public async Task<GetByDynamicQueryResponse> Handle(GetByDynamicQueryRequest request, CancellationToken cancellationToken)
        {
            var queryable= _productRead.GetListByDynamic(request.Dynamic);
            var products = queryable.Skip(request.Page * request.Size).Take(request.Size);
            List<ProductDto> productDtos= _mapper.Map<List<Domain.Entities.Product> ,List<ProductDto>> (await products.ToListAsync());
            return new GetByDynamicQueryResponse()
            {
                Count = queryable.Count(),
                Products = productDtos,
            };
        }
    }
}
