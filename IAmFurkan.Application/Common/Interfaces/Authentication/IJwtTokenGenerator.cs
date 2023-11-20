using IAmFurkan.Domain.Entities;

namespace IAmFurkan.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
