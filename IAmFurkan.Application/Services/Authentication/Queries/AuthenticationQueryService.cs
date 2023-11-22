using ErrorOr;
using IAmFurkan.Application.Common.Interfaces.Authentication;
using IAmFurkan.Application.Common.Interfaces.Persistence;
using IAmFurkan.Domain.Common.Errors;
using IAmFurkan.Domain.Entities;

namespace IAmFurkan.Application.Services.Authentication.Queries;
public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.User.DuplicateEmail;
        }
        if (user.Password != password)
        {
            return new[] { Errors.Authentication.InvalidCredentials };
        }

        string? token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}
