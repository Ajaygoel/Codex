using DoctorAppointments.Domain.Enums;
using DoctorAppointments.Domain.ValueObjects;

namespace DoctorAppointments.Domain.Entities;

public sealed class Appointment
{
    public Appointment(
        Guid id,
        Guid patientId,
        string patientName,
        string tenantId,
        DateTimeOffset scheduledFor,
        string reason,
        AppointmentStatus status,
        Diagnosis? initialDiagnosis)
    {
        Id = id;
        PatientId = patientId;
        PatientName = patientName;
        TenantId = tenantId;
        ScheduledFor = scheduledFor;
        Reason = reason;
        Status = status;
        InitialDiagnosis = initialDiagnosis;
    }

    public Guid Id { get; }
    public Guid PatientId { get; }
    public string PatientName { get; }
    public string TenantId { get; }
    public DateTimeOffset ScheduledFor { get; }
    public string Reason { get; }
    public AppointmentStatus Status { get; private set; }
    public Diagnosis? InitialDiagnosis { get; private set; }

    public void Confirm()
    {
        Status = AppointmentStatus.Confirmed;
    }

    public void Complete(Diagnosis diagnosis)
    {
        InitialDiagnosis = diagnosis;
        Status = AppointmentStatus.Completed;
    }

    public void Cancel()
    {
        Status = AppointmentStatus.Cancelled;
    }
}
