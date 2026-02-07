using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Application.Models;
using DoctorAppointments.Domain.Entities;
using DoctorAppointments.Domain.Enums;
using DoctorAppointments.Domain.ValueObjects;

namespace DoctorAppointments.Application.Services;

public sealed class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repository;
    private readonly ITenantProvider _tenantProvider;

    public AppointmentService(IAppointmentRepository repository, ITenantProvider tenantProvider)
    {
        _repository = repository;
        _tenantProvider = tenantProvider;
    }

    public async Task<IReadOnlyList<AppointmentSummary>> GetUpcomingAsync(CancellationToken cancellationToken)
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

    public async Task<AppointmentSummary> RequestAsync(AppointmentRequest request, CancellationToken cancellationToken)
    {
        var tenantId = _tenantProvider.GetTenantId();
        var patientId = Guid.NewGuid();
        var appointmentId = Guid.NewGuid();
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
