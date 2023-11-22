using ErrorOr;
using IAmFurkan.Application.Common.Interfaces.Authentication;
using IAmFurkan.Application.Common.Interfaces.Persistence;
using IAmFurkan.Domain.Common.Errors;
using IAmFurkan.Domain.Entities;

namespace IAmFurkan.Application.Services.Authentication.Commands;
public class AuthenticationCommandService : IAuthenticationCommandService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) != null)
        {
            return Errors.User.DuplicateEmail;
        }

        User user = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);

        string? token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }
}
