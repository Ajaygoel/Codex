using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Domain.Entities;

namespace DoctorAppointments.Infrastructure.Repositories;

public sealed class InMemoryTenantRepository : ITenantRepository
{
    private readonly List<Tenant> _tenants =
    [
        new Tenant("north-clinic", "North Clinic", "North"),
        new Tenant("central-hospital", "Central Hospital", "Central"),
        new Tenant("west-health", "West Health", "West")
    ];

    public Task<IReadOnlyList<Tenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyList<Tenant>>(_tenants);
    }
}
