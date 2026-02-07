using DoctorAppointments.Application.Models;

namespace DoctorAppointments.Application.Interfaces;

public interface IAppointmentService
{
    Task<IReadOnlyList<AppointmentSummary>> GetUpcomingAsync(CancellationToken cancellationToken);
    Task<AppointmentSummary> RequestAsync(AppointmentRequest request, CancellationToken cancellationToken);
}
