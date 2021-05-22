using DAL.Contexts;
using DAL.Interfaces;
using DAL.Repositories.Interfaces;
using DAL.Repositories.Realizations;
using DAL.Repositories.RealizationsWithIncludes;
using DAL.UoWs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WarehouseService.Handlers;

namespace API.DependenciesResolvers
{
    public static class DalDependencies
    {
        public static void DalDependenciesResolver(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WarehouseContext>(
                options => options.UseSqlServer(VaultHandler.GetDbDataFromVault()));

            services.AddScoped<DbContext, WarehouseContext>();
            services.AddScoped<IUoW, EfUoW>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IProductRepository, ProductRepositoryWithIncludes>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
        }
    }
}