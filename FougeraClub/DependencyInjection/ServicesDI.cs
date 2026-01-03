using FluentValidation;
using FluentValidation.AspNetCore;
using FougeraClub.Services.DTOs.SupplierDtos;
using FougeraClub.Services.IServices;
using FougeraClub.Services.Services;
using FougeraClub.Services.Validations;

namespace FougeraClub.DependencyInjection
{
    public static class ServicesDI
    {
        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            
            services.AddScoped<IValidator<SupplierCreateDto>, SupplierCreateDtoValidation>();
            // Register generic ValidationFilter
            services.AddScoped(typeof(ValidationFilter<>));

            return services;
        }
        public static IServiceCollection AddPurchaesServices(this IServiceCollection services)
        {

            services.AddScoped<IPurchaseItemsServices, PurchaseItemsServices>();
            services.AddScoped<IPurchaseOrdersServices, PurchaseOrdersServices>();
            services.AddScoped<ISupplierServices, SupplierServices>();
            services.AddScoped<IInvoiceServices, InvoiceServices>();

            return services;
        }
    }
}
