using Microsoft.Extensions.DependencyInjection;
using Repositories.Models;
using Repositories.UnitOfWork;
using Services.Implementations;
using Services.Interfaces;

namespace Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Register DbContext
            services.AddScoped<SU25LionDBContext>();

            // Register Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ILionProfileService, LionProfileService>();
            services.AddScoped<ILionTypeService, LionTypeService>();

            return services;
        }
    }
}