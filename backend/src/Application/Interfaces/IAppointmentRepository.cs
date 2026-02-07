using DoctorAppointments.Domain.Entities;

namespace DoctorAppointments.Application.Interfaces;

public interface IAppointmentRepository
{
    Task<IReadOnlyList<Appointment>> GetUpcomingAsync(string tenantId, CancellationToken cancellationToken);
    Task AddAsync(Appointment appointment, CancellationToken cancellationToken);
}
