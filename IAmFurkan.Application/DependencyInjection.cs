using IAmFurkan.Application.Services.Authentication;
using IAmFurkan.Application.Services.Authentication.Commands;
using IAmFurkan.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace IAmFurkan.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
        services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();

        return services;
    }
}
