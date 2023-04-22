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
using ETradeStudy.Percistance.Repositories.Category;
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

            services.AddDbContext<ETradeStudyContext>(options => options.UseNpgsql(Configuration.ConnectionString));
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

            services.AddScoped<ISupplierRead, SupplierRead>();
            services.AddScoped<ISupplierWrite, SupplierWrite>();
            services.AddScoped<IProductRead, ProductRead>();
            services.AddScoped<IProductWrite, ProductWrite>();
            services.AddScoped<IFileRead, FileRead>();
            services.AddScoped<IFileWrite, FileWrite>();
            services.AddScoped<IEndpointRead, EndpointRead>();
            services.AddScoped<IEndpointWrite, EndpointWrite>();
            services.AddScoped<IMenuRead, MenuRead>();
            services.AddScoped<IMenuWrite, MenuWrite>();
            services.AddScoped<IBasketRead, BasketRead>();
            services.AddScoped<IBasketWrite, BasketWrite>();
            services.AddScoped<ICategoryRead ,CategoryRead>();
            services.AddScoped<ICategoryWrite, CategoryWrite>();
            services.AddScoped<ICompletedOrderRead, CompletedOrderRead>();
            services.AddScoped<ICompletedOrderWrite, CompletedOrderWrite>();
            services.AddScoped<IOrderRead, OrderRead>();
            services.AddScoped<IOrderWrite, OrderWrite>();
            services.AddScoped<IBasketItemRead, BasketItemRead>();
            services.AddScoped<IBasketItemWrite, BasketItemWrite>();
            services.AddScoped<IProductImageFileRead, ProdutImageFileRead>();
            services.AddScoped<IProductImageFileWrite, ProductImageFileWrite>();
            services.AddScoped<IInvoceFileRead, InvoiceFileRead>();
            services.AddScoped<IInvoiceFileWrite, InvoiceFileWrite>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IAuthorizationEndpointService,AuthorizationEndpointService>();


        }
    }
}
