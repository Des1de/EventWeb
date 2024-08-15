using System.Security.Claims;
using System.Text;
using EventWeb.Core.Models;
using EventWeb.DataAccess.Contexts;
using EventWeb.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Writers;
using Serilog;

namespace EventWeb.API.Extentions
{
    public static class APIExtention
    {
        public static IServiceCollection ConfigureLogger(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
               .WriteTo.Console()
                .CreateLogger();
          services.AddSerilog();
          return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<EventContext>()
                .AddDefaultTokenProviders();
            return services;
        }
        public static IServiceCollection ConfigureMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(APIExtention).Assembly);
            return services;
        }

        public static IServiceCollection ConfigureAPIAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(options =>{
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            } )
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options => 
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions!.SecretKey)),
                        RoleClaimType = ClaimTypes.Role 
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context => 
                        {
                            context.Token = context.Request.Cookies["NotJwtToken"];
                            return Task.CompletedTask;
                        }
                    };
                }); 

            services.AddAuthorization();
            return services; 
        }
    }
}