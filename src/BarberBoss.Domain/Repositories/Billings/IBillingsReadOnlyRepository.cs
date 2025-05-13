using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories.Billings;

public interface IBillingsReadOnlyRepository
{
    Task<Billing?> FindById(long id);
    Task<List<Billing>> FilterByMonth(DateOnly date);
}
