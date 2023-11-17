using IAmFurkan.Application.Common.Interfaces.Authentication;

namespace IAmFurkan.Application.Services.Authentication;
public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        Guid userId = Guid.NewGuid();

        string? token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
        return new(
            userId,
            firstName,
            lastName,
            email,
            token);
    }
    public AuthenticationResult Login(string email, string password)
    {
        return new(
            Guid.NewGuid(),
            "",
            "",
            email,
            "");
    }
}
