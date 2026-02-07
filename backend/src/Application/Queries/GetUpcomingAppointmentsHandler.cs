using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Application.Models;

namespace DoctorAppointments.Application.Queries;

public sealed class GetUpcomingAppointmentsHandler
    : IQueryHandler<GetUpcomingAppointments, IReadOnlyList<AppointmentSummary>>
{
    private readonly IAppointmentRepository _repository;
    private readonly ITenantProvider _tenantProvider;

    public GetUpcomingAppointmentsHandler(IAppointmentRepository repository, ITenantProvider tenantProvider)
    {
        _repository = repository;
        _tenantProvider = tenantProvider;
    }

    public async Task<IReadOnlyList<AppointmentSummary>> HandleAsync(
        GetUpcomingAppointments query,
        CancellationToken cancellationToken)
    {
        var tenantId = _tenantProvider.GetTenantId();
        var appointments = await _repository.GetUpcomingAsync(tenantId, cancellationToken);

        return appointments
            .Select(appointment => new AppointmentSummary(
                appointment.Id,
                appointment.PatientId,
                appointment.PatientName,
                appointment.ScheduledFor,
                appointment.Reason,
                appointment.Status.ToString(),
                appointment.InitialDiagnosis?.Summary))
            .ToList();
    }
}
