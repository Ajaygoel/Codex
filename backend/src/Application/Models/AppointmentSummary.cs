namespace DoctorAppointments.Application.Models;

public sealed record AppointmentSummary(
    Guid AppointmentId,
    Guid PatientId,
    string PatientName,
    DateTimeOffset ScheduledFor,
    string Reason,
    string Status,
    string? InitialDiagnosisSummary
);
