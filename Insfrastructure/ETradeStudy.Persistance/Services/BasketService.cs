        using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.Repositories;
using ETradeStudy.Application.Repositories.Basket;
using ETradeStudy.Application.Repositories.BasketItem;
using ETradeStudy.Application.ViewModel.Baskets;
using ETradeStudy.Domain.Entities;
using ETradeStudy.Domain.Entities.Identity;
using ETradeStudy.Percistance.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly IOrderRead _orderRead;
        private readonly IBasketWrite _basketWrite;
        private readonly IBasketRead _basketRead;
        private readonly IBasketItemWrite _basketItemWrite;
        private readonly IBasketItemRead _basketItemRead;

        public Basket? GetUserActiveBasket
        {
            get
            {
                return ContextUser().Result;
            }
        }

        public BasketService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IOrderRead orderRead, IBasketWrite basketWrite, IBasketItemWrite basketItemWrite, IBasketItemRead basketItemRead, IBasketRead basketRead)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _orderRead = orderRead;
            _basketWrite = basketWrite;
            _basketItemWrite = basketItemWrite;
            _basketItemRead = basketItemRead;
            _basketRead = basketRead;
        }

        private async Task<Basket?> ContextUser()
        {
            var userName = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if (!string.IsNullOrEmpty(userName))
            {
                AppUser? appUser = await _userManager.Users
                        .Include(u => u.Baskets)
                        .FirstOrDefaultAsync(u => u.UserName == userName);

                var _basket = from basket in appUser.Baskets
                              join order in _orderRead.Table
                              on basket.Id equals order.Id into BasketOrders
                              from order in BasketOrders.DefaultIfEmpty()
                              select new
                              {
                                  Basket = basket,
                                  Order = order
                              };

                Basket? targetBasket = null;
                if (_basket.Any(b => b.Order is null))
                    targetBasket = _basket.FirstOrDefault(b => b.Order is null)?.Basket;
                else
                {
                    targetBasket = new();
                    appUser.Baskets.Add(targetBasket);
                }

                await _basketWrite.SaveAsync();
                return targetBasket;
            }
            throw new Exception("Beklenmeyen bir hata ile karşılaşıldı...");
        }

        public async Task AddItemToBasketAsync(VM_Create_BasketItem basketItem)
        {
            Basket? basket = await ContextUser();
            if (basket != null)
            {
                BasketItem _basketItem = await _basketItemRead.GetSingleAsync(bi => bi.BasketId == basket.Id && bi.ProductId == Guid.Parse(basketItem.ProductId));

                if (_basketItem != null)
                    _basketItem.Quantity++;
                else
                    await _basketItemWrite.AddAsync(new()
                    {
                        BasketId = basket.Id,
                        ProductId = Guid.Parse(basketItem.ProductId),
                        Quantity = basketItem.Quantity,
                    });
                await _basketItemWrite.SaveAsync();
            }
        }

        public async Task<List<BasketItem>> GetBasketItemsAsync()
        {
            Basket? basket = await ContextUser();
            Basket? result = await _basketRead.Table
                .Include(b => b.BasketItems)
                .ThenInclude(bi => bi.Product)
                .FirstOrDefaultAsync(b => b.Id == basket.Id);

            return result.BasketItems.ToList();
        }

        public async Task RemoveBasketItemAsync(string basketItemId)
        {
            BasketItem? basketItem = await _basketItemRead.GetByIdAsync(basketItemId);
            if (basketItem != null)
            {
                _basketItemWrite.Remove(basketItem);
                await _basketItemWrite.SaveAsync();
            }
        }

        public async Task UpdateQuantityAsync(VM_Update_BasketItem basketItem)
        {
            BasketItem _basketItem = await _basketItemRead.GetByIdAsync(basketItem.BasketItemId);
            if (_basketItem != null)
            {
                _basketItem.Quantity = basketItem.Quantity;
                await _basketItemWrite.SaveAsync();
            }
        }

    }
}
