using DoctorAppointments.Application.Commands;
using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Application.Models;
using DoctorAppointments.Application.Queries;
using DoctorAppointments.Domain.Entities;
using DoctorAppointments.Domain.Enums;
using DoctorAppointments.Domain.ValueObjects;
using Xunit;

namespace DoctorAppointments.Application.Tests;

public sealed class AppointmentHandlersTests
{
    [Fact]
    public async Task RequestAppointmentHandler_creates_summary_and_persists_appointment()
    {
        var repository = new CapturingAppointmentRepository();
        var tenantProvider = new FixedTenantProvider("north-clinic");
        var handler = new RequestAppointmentHandler(repository, tenantProvider);
        var request = new AppointmentRequest(
            new PatientInput("Jamie", "Smith", new DateOnly(1990, 1, 2)),
            DateTimeOffset.UtcNow.AddDays(2),
            "Annual checkup",
            new DiagnosisInput("Mild cough", "Monitor symptoms"));

        var result = await handler.HandleAsync(new RequestAppointment(request), CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result.AppointmentId);
        Assert.NotEqual(Guid.Empty, result.PatientId);
        Assert.Equal("Jamie Smith", result.PatientName);
        Assert.Equal("Annual checkup", result.Reason);
        Assert.Equal("Requested", result.Status);
        Assert.Equal("Mild cough", result.InitialDiagnosisSummary);
        Assert.NotNull(repository.LastAppointment);
        Assert.Equal("north-clinic", repository.LastAppointment?.TenantId);
    }

    [Fact]
    public async Task GetUpcomingAppointmentsHandler_returns_mapped_summaries()
    {
        var appointment = new Appointment(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Taylor Quinn",
            "north-clinic",
            DateTimeOffset.UtcNow.AddDays(1),
            "Follow-up",
            AppointmentStatus.Confirmed,
            new Diagnosis("Headache", "Hydration", DateTimeOffset.UtcNow));
        var repository = new StubAppointmentRepository(appointment);
        var tenantProvider = new FixedTenantProvider("north-clinic");
        var handler = new GetUpcomingAppointmentsHandler(repository, tenantProvider);

        var results = await handler.HandleAsync(new GetUpcomingAppointments(), CancellationToken.None);

        var summary = Assert.Single(results);
        Assert.Equal(appointment.Id, summary.AppointmentId);
        Assert.Equal("Taylor Quinn", summary.PatientName);
        Assert.Equal("Confirmed", summary.Status);
        Assert.Equal("Headache", summary.InitialDiagnosisSummary);
        Assert.Equal("north-clinic", repository.LastTenantId);
    }

    private sealed class FixedTenantProvider : ITenantProvider
    {
        private readonly string _tenantId;

        public FixedTenantProvider(string tenantId)
        {
            _tenantId = tenantId;
        }

        public string GetTenantId() => _tenantId;
    }

    private sealed class CapturingAppointmentRepository : IAppointmentRepository
    {
        public Appointment? LastAppointment { get; private set; }

        public Task<IReadOnlyList<Appointment>> GetUpcomingAsync(string tenantId, CancellationToken cancellationToken)
            => Task.FromResult<IReadOnlyList<Appointment>>(Array.Empty<Appointment>());

        public Task AddAsync(Appointment appointment, CancellationToken cancellationToken)
        {
            LastAppointment = appointment;
            return Task.CompletedTask;
        }
    }

    private sealed class StubAppointmentRepository : IAppointmentRepository
    {
        private readonly IReadOnlyList<Appointment> _appointments;

        public StubAppointmentRepository(params Appointment[] appointments)
        {
            _appointments = appointments;
        }

        public string? LastTenantId { get; private set; }

        public Task<IReadOnlyList<Appointment>> GetUpcomingAsync(string tenantId, CancellationToken cancellationToken)
        {
            LastTenantId = tenantId;
            return Task.FromResult(_appointments);
        }

        public Task AddAsync(Appointment appointment, CancellationToken cancellationToken)
            => Task.CompletedTask;
    }
}
