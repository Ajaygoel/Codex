using DoctorAppointments.Domain.Entities;
using DoctorAppointments.Domain.Enums;
using DoctorAppointments.Domain.ValueObjects;
using Xunit;

namespace DoctorAppointments.Domain.Tests;

public sealed class AppointmentTests
{
    [Fact]
    public void Confirm_sets_status_to_confirmed()
    {
        var appointment = BuildAppointment();

        appointment.Confirm();

        Assert.Equal(AppointmentStatus.Confirmed, appointment.Status);
    }

    [Fact]
    public void Complete_sets_diagnosis_and_status()
    {
        var appointment = BuildAppointment();
        var diagnosis = new Diagnosis("Flu", "Rest and fluids", DateTimeOffset.UtcNow);

        appointment.Complete(diagnosis);

        Assert.Equal(AppointmentStatus.Completed, appointment.Status);
        Assert.Equal(diagnosis, appointment.InitialDiagnosis);
    }

    [Fact]
    public void Cancel_sets_status_to_cancelled()
    {
        var appointment = BuildAppointment();

        appointment.Cancel();

        Assert.Equal(AppointmentStatus.Cancelled, appointment.Status);
    }

    private static Appointment BuildAppointment() => new(
        Guid.NewGuid(),
        Guid.NewGuid(),
        "Alex Patient",
        "north-clinic",
        DateTimeOffset.UtcNow.AddDays(1),
        "Checkup",
        AppointmentStatus.Requested,
        null);
}
