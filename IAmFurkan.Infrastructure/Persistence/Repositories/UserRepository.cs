using IAmFurkan.Application.Common.Interfaces.Persistence;
using IAmFurkan.Domain.Entities;

namespace IAmFurkan.Infrastructure.Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private readonly IAmFurkanDbContext _context;

    public UserRepository(IAmFurkanDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Add(user);
        _context.SaveChanges();
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(x=> x.Email == email);
    }
}
