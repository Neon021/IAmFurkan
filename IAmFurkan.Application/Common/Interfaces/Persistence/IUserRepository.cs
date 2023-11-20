using IAmFurkan.Domain.Entities;

namespace IAmFurkan.Application.Common.Interfaces.Persistence;
public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}
