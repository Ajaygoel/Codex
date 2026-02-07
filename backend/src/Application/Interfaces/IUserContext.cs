namespace DoctorAppointments.Application.Interfaces;

public interface IUserContext
{
    string UserId { get; }
    string DisplayName { get; }
    IReadOnlyCollection<string> Roles { get; }
}
