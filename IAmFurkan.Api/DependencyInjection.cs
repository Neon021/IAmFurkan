using IAmFurkan.Api.Common.Errors;
using IAmFurkan.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace IAmFurkan.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, IAmFurkanProblemDetailsFactory>();
            services.AddMappings();

            return services;
        }
    }
}
