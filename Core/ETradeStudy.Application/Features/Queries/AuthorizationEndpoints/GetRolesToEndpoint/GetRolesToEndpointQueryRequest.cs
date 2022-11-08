﻿using MediatR;

namespace ETradeStudy.Application.Features.Queries.AuthorizationEndpoints.GetRolesToEndpoint
{
    public class GetRolesToEndpointQueryRequest:IRequest<GetRolesToEndpointQueryResponse>
    {
        public string Code { get; set; }
        public string Menu { get; set; }
    }
}