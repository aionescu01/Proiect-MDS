using Microsoft.Extensions.DependencyInjection;
using System;
using ProjectV1.DAL.Seeders;
using ProjectV1.Services.AuthServices;


namespace ProjectV1.Services
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
        }

        public static void AddSeeders(this IServiceCollection services)
        {
            services.AddTransient<InitialSeed>();
        }
    }
}
