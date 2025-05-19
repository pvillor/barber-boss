using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Services;

namespace BarberBoss.Infrastructure.Services;

public class LoggedUser : ILoggedUser
{
    public Task<User> Get()
    {
        throw new NotImplementedException();
    }
}
