namespace DoctorAppointments.Domain.Entities;

public sealed class Tenant
{
    public Tenant(string id, string name, string region)
    {
        Id = id;
        Name = name;
        Region = region;
    }

    public string Id { get; }
    public string Name { get; }
    public string Region { get; }
}
