using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Billings;

public interface IBillingsUpdateOnlyRepository
{
    Task<Billing?> FindById(long id);
    void Update(Billing billing);
}
