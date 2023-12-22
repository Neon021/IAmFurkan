using IAmFurkan.Domain.Entities;

namespace IAmFurkan.Application.Authentication.Common
{
    public record AuthenticationResult(
    User User,
    string Token);
}
