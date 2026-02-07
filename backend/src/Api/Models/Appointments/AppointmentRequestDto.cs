namespace DoctorAppointments.Api.Models.Appointments;

public sealed record AppointmentRequestDto(
    PatientInputDto Patient,
    DateTimeOffset ScheduledFor,
    string Reason,
    DiagnosisInputDto? InitialDiagnosis
);

public sealed record PatientInputDto(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth
);

public sealed record DiagnosisInputDto(
    string Summary,
    string Notes
);
