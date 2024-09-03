using EventWeb.API.Extentions;
using EventWeb.Application.Extentions;
using EventWeb.Core.Abstractions.Repositories;
using EventWeb.DataAccess.Contexts;
using EventWeb.DataAccess.Repositories;
using EventWeb.Infrastructure;
using EventWeb.Infrastructure.Extentions;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureLogger()
            .ConfigureIdentity()
            .ConfigureMapper()
            .ConfigureAPIAuthentication(builder.Configuration)
            .ConfigureAutoFluentValidation(); 

builder.Services.ConfigureInfrastructure(); 

builder.Services.AddDbContext<EventContext>(
    options => 
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(EventContext)), 
        b => b.MigrationsAssembly("EventWeb.API")); 
    }); 

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>() 
    .ConfigureUseCases();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always
});

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
