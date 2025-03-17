using Lotto.Data.UnitOfWorks;
using Lotto.Service.Services.Advertisements;
using Lotto.WebApi.ApiServices;
using Lotto.WebApi.ApiServices.Advertisements;
using Lotto.WebApi.ApiServices.Numbers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Lotto.WebApi.Extensions
{
    public static class ServicesCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAdvertisementService, AdvertisementService>();
            services.AddScoped<INumberService, NumberService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<INumberApiService, NumberApiService>();
           // services.AddScoped<IAdvertisementApiService, AdvertisementApiService>();
        }
    }
}
