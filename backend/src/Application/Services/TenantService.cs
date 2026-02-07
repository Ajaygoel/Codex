using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Domain.Entities;

namespace DoctorAppointments.Application.Services;

public sealed class TenantService
{
    private readonly ITenantRepository _tenantRepository;

    public TenantService(ITenantRepository tenantRepository)
    {
        _tenantRepository = tenantRepository;
    }

    public Task<IReadOnlyList<Tenant>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _tenantRepository.GetAllAsync(cancellationToken);
    }
}
