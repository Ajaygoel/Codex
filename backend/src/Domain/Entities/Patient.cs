namespace DoctorAppointments.Domain.Entities;

public sealed class Patient
{
    public Patient(Guid id, string firstName, string lastName, DateOnly dateOfBirth)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public DateOnly DateOfBirth { get; }

    public string FullName => $"{FirstName} {LastName}";
}
