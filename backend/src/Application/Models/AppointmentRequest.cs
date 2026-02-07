namespace DoctorAppointments.Application.Models;

public sealed record AppointmentRequest(
    PatientInput Patient,
    DateTimeOffset ScheduledFor,
    string Reason,
    DiagnosisInput? InitialDiagnosis
);

public sealed record PatientInput(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth
);

public sealed record DiagnosisInput(
    string Summary,
    string Notes
);
