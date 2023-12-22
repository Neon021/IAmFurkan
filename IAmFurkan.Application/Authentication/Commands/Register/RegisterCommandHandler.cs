using ErrorOr;
using IAmFurkan.Application.Authentication.Common;
using IAmFurkan.Application.Common.Interfaces.Authentication;
using IAmFurkan.Application.Common.Interfaces.Persistence;
using IAmFurkan.Domain.Common.Errors;
using IAmFurkan.Domain.Entities;
using MediatR;

namespace IAmFurkan.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if(_userRepository.GetUserByEmail(command.Email) != null)
            {
                return Errors.User.DuplicateEmail;
            }

            User user = new()
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.password
            };

            _userRepository.Add(user);

            string token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
