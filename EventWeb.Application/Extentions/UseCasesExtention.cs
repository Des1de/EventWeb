using EventWeb.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace EventWeb.Application.Extentions
{
    public static class UseCasesExtention
    {
        public static IServiceCollection ConfigureUseCases(this IServiceCollection services)
        {
            services.ConfigureUserUseCases()
                    .ConfigureEventUseCases()
                    .ConfigureCategoryUseCases()
                    .ConfigureParticipationUseCases();
            return services; 
        }

        private static IServiceCollection ConfigureUserUseCases(this IServiceCollection services)
        {
            services.AddScoped<GetAllUsersUseCase>()
                    .AddScoped<RegisterUserUseCase>()
                    .AddScoped<LoginUserUseCase>()
                    .AddScoped<RefreshUserUseCase>(); 
            return services; 
        }

        private static IServiceCollection ConfigureEventUseCases(this IServiceCollection services)
        {
            services.AddScoped<CreateEventUseCase>()
                    .AddScoped<DeleteEventUseCase>()
                    .AddScoped<GetAllEventsUseCase>()
                    .AddScoped<GetEventByIdUseCase>()
                    .AddScoped<GetEventByNameUseCase>()
                    .AddScoped<UpdateEventUseCase>(); 
            return services; 
        }

        private static IServiceCollection ConfigureCategoryUseCases(this IServiceCollection services)
        {
            services.AddScoped<CreateCategoryUseCase>()
                    .AddScoped<DeleteCategoryUseCase>()
                    .AddScoped<GetAllCategoriesUseCase>()
                    .AddScoped<GetCategoryByIdUseCase>()
                    .AddScoped<GetCategoryByNameUseCase>()
                    .AddScoped<UpdateCategoryUseCase>(); 
            return services; 
        }

        private static IServiceCollection ConfigureParticipationUseCases(this IServiceCollection services)
        {
            services.AddScoped<CreateParticipationUseCase>()
                    .AddScoped<DeleteParticipationUseCase>()
                    .AddScoped<GetParticipationsByUserAndEventIdUseCase>()
                    .AddScoped<GetParticipationsByEventIdUseCase>()
                    .AddScoped<GetParticipationsByUserIdUseCase>(); 
            return services; 
        }
    }
}