using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Users;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistsActiveUserWithEmail(string email);
    Task<User?> GetUserByEmail(string email);
}
