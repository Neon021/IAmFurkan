using ErrorOr;
using IAmFurkan.Application.Authentication.Common;
using MediatR;

namespace IAmFurkan.Application.Authentication.Commands.Register;
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string password) : IRequest<ErrorOr<AuthenticationResult>>;