using FluentValidation;
using FluentValidation.AspNetCore;
using FougeraClub.Services.DTOs.SupplierDtos;
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
    }
}
