using ErrorOr;
using IAmFurkan.Application.Authentication.Common;
using IAmFurkan.Application.Common.Interfaces.Authentication;
using IAmFurkan.Application.Common.Interfaces.Persistence;
using IAmFurkan.Domain.Common.Errors;
using IAmFurkan.Domain.Entities;
using MediatR;

namespace IAmFurkan.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            if(_userRepository.GetUserByEmail(query.Email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }

            if(user.Password != query.Password)
            {
                return new[] { Errors.Authentication.InvalidCredentials };
            }

            string? token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
