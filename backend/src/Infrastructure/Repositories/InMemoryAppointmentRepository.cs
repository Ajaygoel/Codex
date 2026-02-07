using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Domain.Entities;

namespace DoctorAppointments.Infrastructure.Repositories;

public sealed class InMemoryAppointmentRepository : IAppointmentRepository
{
    private readonly List<Appointment> _appointments = new();

    public Task<IReadOnlyList<Appointment>> GetUpcomingAsync(CancellationToken cancellationToken)
    {
        var upcoming = _appointments
            .OrderBy(appointment => appointment.ScheduledFor)
            .ToList();

        return Task.FromResult<IReadOnlyList<Appointment>>(upcoming);
    }

    public Task AddAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        _appointments.Add(appointment);
        return Task.CompletedTask;
    }
}
