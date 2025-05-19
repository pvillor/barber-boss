using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Services;

public interface ILoggedUser
{
    Task<User> Get();
}
