using ETradeAPI.SignalR.Hubs;
using ETradeStudy.Application.Abstractions.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.SignalR.HubServices
{
    public class ProductHubService : IProductHubService
    {
        private readonly IHubContext<ProductHub> _context;

        public ProductHubService(IHubContext<ProductHub> context)
        {
            _context = context;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
            await _context.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);
        }
    }
}
