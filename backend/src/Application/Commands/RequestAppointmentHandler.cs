using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Application.Models;
using DoctorAppointments.Domain.Entities;
using DoctorAppointments.Domain.Enums;
using DoctorAppointments.Domain.ValueObjects;

namespace DoctorAppointments.Application.Commands;

public sealed class RequestAppointmentHandler : ICommandHandler<RequestAppointment, AppointmentSummary>
{
    private readonly IAppointmentRepository _repository;
    private readonly ITenantProvider _tenantProvider;

    public RequestAppointmentHandler(IAppointmentRepository repository, ITenantProvider tenantProvider)
    {
        _repository = repository;
        _tenantProvider = tenantProvider;
    }

    public async Task<AppointmentSummary> HandleAsync(RequestAppointment command, CancellationToken cancellationToken)
    {
        var tenantId = _tenantProvider.GetTenantId();
        var patientId = Guid.NewGuid();
        var appointmentId = Guid.NewGuid();
        var request = command.Request;
        var diagnosis = request.InitialDiagnosis is null
            ? null
            : new Diagnosis(
                request.InitialDiagnosis.Summary,
                request.InitialDiagnosis.Notes,
                DateTimeOffset.UtcNow);

        var patientName = $"{request.Patient.FirstName} {request.Patient.LastName}";

        var appointment = new Appointment(
            appointmentId,
            patientId,
            patientName,
            tenantId,
            request.ScheduledFor,
            request.Reason,
            AppointmentStatus.Requested,
            diagnosis);

        await _repository.AddAsync(appointment, cancellationToken);

        return new AppointmentSummary(
            appointment.Id,
            patientId,
            patientName,
            appointment.ScheduledFor,
            appointment.Reason,
            appointment.Status.ToString(),
            appointment.InitialDiagnosis?.Summary);
    }
}
