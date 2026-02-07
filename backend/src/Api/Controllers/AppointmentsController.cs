using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointments.Api.Controllers;

[ApiController]
[Route("api/appointments")]
public sealed class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AppointmentSummary>>> GetUpcoming(CancellationToken cancellationToken)
    {
        var appointments = await _appointmentService.GetUpcomingAsync(cancellationToken);
        return Ok(appointments);
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentSummary>> RequestAppointment(
        [FromBody] AppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var appointment = await _appointmentService.RequestAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetUpcoming), new { id = appointment.AppointmentId }, appointment);
    }
}
