using FougeraClub.Core.IRespository;
using FougeraClub.Infrastructure.Repository;

namespace FougeraClub.DependencyInjection
{
    public static  class InfrastructureDI
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();  
            services.AddScoped<IPurchaseItemsRepository, PurchaseItemsRepository>();
            services.AddScoped<IPurchaseOrdersRepository, PurchaseOrdersRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            return services;
        }
    }
}
