using IAmFurkan.Application.Common.Interfaces.Authentication;
using IAmFurkan.Application.Common.Interfaces.Persistence;
using IAmFurkan.Application.Common.Interfaces.Services;
using IAmFurkan.Infrastructure.Authentication;
using IAmFurkan.Infrastructure.Persistence;
using IAmFurkan.Infrastructure.Persistence.Repositories;
using IAmFurkan.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IAmFurkan.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IAmFurkanDbContext>(options =>
                options.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=IAmFurkan"));

        services.AddScoped<IUserRepository, UserRepository>();

        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
