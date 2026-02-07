namespace DoctorAppointments.Api.Models.Appointments;

public sealed record AppointmentSummaryDto(
    Guid AppointmentId,
    Guid PatientId,
    string PatientName,
    DateTimeOffset ScheduledFor,
    string Reason,
    string Status,
    string? InitialDiagnosisSummary
);
