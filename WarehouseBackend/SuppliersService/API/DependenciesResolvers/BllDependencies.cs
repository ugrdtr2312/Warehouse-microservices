using BLL.Services.Interfaces;
using BLL.Services.Realizations;
using Microsoft.Extensions.DependencyInjection;

namespace API.DependenciesResolvers
{
    public static class BllDependencies
    {
        public static void BllDependenciesResolver(this IServiceCollection services)
        {
            services.AddScoped<ISupplierService, SupplierService>();
        }
    }
}