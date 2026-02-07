namespace DoctorAppointments.Domain.ValueObjects;

public sealed record Diagnosis(
    string Summary,
    string Notes,
    DateTimeOffset RecordedAt
);
