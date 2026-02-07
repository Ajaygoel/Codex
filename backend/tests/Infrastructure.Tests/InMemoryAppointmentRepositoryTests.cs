using DoctorAppointments.Domain.Entities;
using DoctorAppointments.Domain.Enums;
using DoctorAppointments.Infrastructure.Repositories;
using Xunit;

namespace DoctorAppointments.Infrastructure.Tests;

public sealed class InMemoryAppointmentRepositoryTests
{
    [Fact]
    public async Task GetUpcomingAsync_filters_by_tenant_and_orders_by_schedule()
    {
        var repository = new InMemoryAppointmentRepository();
        var first = BuildAppointment("north-clinic", DateTimeOffset.UtcNow.AddHours(4));
        var second = BuildAppointment("north-clinic", DateTimeOffset.UtcNow.AddHours(1));
        var otherTenant = BuildAppointment("south-clinic", DateTimeOffset.UtcNow.AddHours(2));

        await repository.AddAsync(first, CancellationToken.None);
        await repository.AddAsync(second, CancellationToken.None);
        await repository.AddAsync(otherTenant, CancellationToken.None);

        var results = await repository.GetUpcomingAsync("north-clinic", CancellationToken.None);

        Assert.Equal(2, results.Count);
        Assert.Equal(second.Id, results[0].Id);
        Assert.Equal(first.Id, results[1].Id);
    }

    private static Appointment BuildAppointment(string tenantId, DateTimeOffset scheduledFor) => new(
        Guid.NewGuid(),
        Guid.NewGuid(),
        "Casey Patient",
        tenantId,
        scheduledFor,
        "Checkup",
        AppointmentStatus.Requested,
        null);
}
