using ErrorOr;
using IAmFurkan.Application.Authentication.Common;
using MediatR;

namespace IAmFurkan.Application.Authentication.Queries.Login
{
    public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
