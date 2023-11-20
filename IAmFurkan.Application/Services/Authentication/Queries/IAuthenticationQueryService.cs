namespace IAmFurkan.Application.Services.Authentication.Queries;
public interface IAuthenticationQueryService
{
    AuthenticationResult Login(string email, string password);
}
