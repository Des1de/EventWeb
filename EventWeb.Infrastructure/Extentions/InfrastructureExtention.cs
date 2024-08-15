using Microsoft.Extensions.DependencyInjection;

namespace EventWeb.Infrastructure.Extentions
{
    public static class InfrastructureExtention
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider, JwtProvider>(); 
            services.AddScoped<IFileService, FileService>(); 

            return services; 
        }


    }
}