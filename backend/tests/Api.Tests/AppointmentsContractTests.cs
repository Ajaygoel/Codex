using System.Net;
using System.Net.Http.Json;
using DoctorAppointments.Api.Models.Appointments;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace DoctorAppointments.Api.Tests;

public sealed class AppointmentsContractTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public AppointmentsContractTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Get_upcoming_requires_sso_header_and_returns_empty_list()
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("X-User-Id", "user-123");

        var response = await client.GetAsync("/api/appointments");

        response.EnsureSuccessStatusCode();
        var payload = await response.Content.ReadFromJsonAsync<List<AppointmentSummaryDto>>();
        Assert.NotNull(payload);
        Assert.Empty(payload!);
    }

    [Fact]
    public async Task Request_appointment_returns_created_summary()
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("X-User-Id", "user-123");
        client.DefaultRequestHeaders.Add("X-Tenant-Id", "north-clinic");

        var request = new AppointmentRequestDto(
            new PatientInputDto("Morgan", "Lee", new DateOnly(1988, 4, 12)),
            DateTimeOffset.UtcNow.AddDays(3),
            "Consultation",
            new DiagnosisInputDto("Cough", "Check vitals"));

        var response = await client.PostAsJsonAsync("/api/appointments", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var payload = await response.Content.ReadFromJsonAsync<AppointmentSummaryDto>();
        Assert.NotNull(payload);
        Assert.Equal("Morgan Lee", payload!.PatientName);
        Assert.Equal("Consultation", payload.Reason);
        Assert.Equal("Requested", payload.Status);
        Assert.Equal("Cough", payload.InitialDiagnosisSummary);
    }
}
