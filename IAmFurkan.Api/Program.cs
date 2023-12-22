using IAmFurkan.Api;
using IAmFurkan.Application;
using IAmFurkan.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());

    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}