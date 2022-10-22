﻿using MediatR;

namespace ETradeStudy.Application.Features.Queries.Order.GetOrderById
{
    public class GetOrderByIdQueryRequest:IRequest<GetOrderByIdQueryResponse>
    {
        public string Id { get; set; }

    }
}