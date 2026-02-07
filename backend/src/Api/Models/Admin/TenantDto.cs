namespace DoctorAppointments.Api.Models.Admin;

public sealed record TenantDto(
    string Id,
    string Name,
    string Region
);
