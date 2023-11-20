using IAmFurkan.Application;
using IAmFurkan.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}