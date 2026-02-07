using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Domain.Entities;

namespace DoctorAppointments.Application.Queries;

public sealed class GetTenantsHandler : IQueryHandler<GetTenants, IReadOnlyList<Tenant>>
{
    private readonly ITenantRepository _tenantRepository;

    public GetTenantsHandler(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public Task<IReadOnlyList<Tenant>> HandleAsync(GetTenants query, CancellationToken cancellationToken)
    {
        return _tenantRepository.GetAllAsync(cancellationToken);
    }
}
