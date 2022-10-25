using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.Abstractions.Services.Authentication;
using ETradeStudy.Application.Repositories;
using ETradeStudy.Application.Repositories.Basket;
using ETradeStudy.Application.Repositories.BasketItem;
using ETradeStudy.Application.Repositories.CompletedOrder;
using ETradeStudy.Domain.Entities.Identity;
using ETradeStudy.Percistance.Contexts;
using ETradeStudy.Percistance.Repositories;
using ETradeStudy.Percistance.Repositories.Basket;
using ETradeStudy.Percistance.Repositories.BasketItem;
using ETradeStudy.Percistance.Repositories.CompletedOrder;
using ETradeStudy.Percistance.Repositories.File;
using ETradeStudy.Percistance.Repositories.ProductImage;
using ETradeStudy.Percistance.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Percistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {

            services.AddDbContext<ETradeStudyContext>(options => options.UseNpgsql(Configuration.ConnectionString), ServiceLifetime.Singleton);
            services.AddIdentity<AppUser, AppRole>(options =>
             {
                 options.Password.RequiredLength = 3;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireDigit = false;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase = false;
             }).AddEntityFrameworkStores<ETradeStudyContext>()
             .AddDefaultTokenProviders();


            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddSingleton<ISupplierRead, SupplierRead>();
            services.AddSingleton<ISupplierWrite, SupplierWrite>();

            services.AddSingleton<IProductRead, ProductRead>();
            services.AddSingleton<IProductWrite, ProductWrite>();


            services.AddSingleton<IFileRead, FileRead>();
            services.AddSingleton<IFileWrite, FileWrite>();


            services.AddSingleton<IBasketRead, BasketRead>();
            services.AddSingleton<IBasketWrite, BasketWrite>();


            services.AddSingleton<ICompletedOrderRead, CompletedOrderRead>();
            services.AddSingleton<ICompletedOrderWrite, CompletedOrderWrite>();

            services.AddSingleton<IOrderRead, OrderRead>();
            services.AddSingleton<IOrderWrite, OrderWrite>();

            services.AddSingleton<IBasketItemRead, BasketItemRead>();
            services.AddSingleton<IBasketItemWrite, BasketItemWrite>();


            services.AddSingleton<IProductImageFileRead, ProdutImageFileRead>();
            services.AddSingleton<IProductImageFileWrite, ProductImageFileWrite>();


            services.AddSingleton<IInvoceFileRead, InvoiceFileRead>();
            services.AddSingleton<IInvoiceFileWrite, InvoiceFileWrite>();

            services.AddScoped<IBasketService, BasketService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();

            services.AddScoped<IOrderService, OrderService>();

        }
    }
}
