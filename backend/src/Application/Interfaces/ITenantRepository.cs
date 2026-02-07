using DoctorAppointments.Domain.Entities;

namespace DoctorAppointments.Application.Interfaces;

public interface ITenantRepository
{
    Task<IReadOnlyList<Tenant>> GetAllAsync(CancellationToken cancellationToken);
}
