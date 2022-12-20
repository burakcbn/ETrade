using ETradeStudy.Application.Abstractions;
using ETradeStudy.Application.Abstractions.Services;
using ETradeStudy.Application.Abstractions.Services.Configurations;
using ETradeStudy.Application.Abstractions.Storage;
using ETradeStudy.Application.Abstractions.Token;
using ETradeStudy.Infrastructure.Enums;
using ETradeStudy.Infrastructure.Services;
using ETradeStudy.Infrastructure.Services.Configurations;
using ETradeStudy.Infrastructure.Services.Storage;
using ETradeStudy.Infrastructure.Services.Storage.Local;
using ETradeStudy.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeStudy.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IQRCodeService, QRCodeService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection services, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.LocalStorage:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;

                case StorageType.Azure:
                    break;

                case StorageType.AWS:
                    break;

                default:
                    services.AddScoped<IStorage, LocalStorage>(); break;
            }
        }
    }
}
