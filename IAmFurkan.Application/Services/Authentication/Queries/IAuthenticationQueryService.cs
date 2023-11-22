using ErrorOr;

namespace IAmFurkan.Application.Services.Authentication.Queries;
public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}
