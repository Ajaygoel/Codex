using DoctorAppointments.Application.Commands;
using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Application.Models;
using DoctorAppointments.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointments.Api.Controllers;

[ApiController]
[Route("api/appointments")]
public sealed class AppointmentsController : ControllerBase
{
    private readonly IQueryHandler<GetUpcomingAppointments, IReadOnlyList<AppointmentSummary>> _getUpcomingHandler;
    private readonly ICommandHandler<RequestAppointment, AppointmentSummary> _requestHandler;

    public AppointmentsController(
        IQueryHandler<GetUpcomingAppointments, IReadOnlyList<AppointmentSummary>> getUpcomingHandler,
        ICommandHandler<RequestAppointment, AppointmentSummary> requestHandler)
    {
        _getUpcomingHandler = getUpcomingHandler;
        _requestHandler = requestHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AppointmentSummary>>> GetUpcoming(CancellationToken cancellationToken)
    {
        var appointments = await _getUpcomingHandler.HandleAsync(new GetUpcomingAppointments(), cancellationToken);
        return Ok(appointments);
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentSummary>> RequestAppointment(
        [FromBody] AppointmentRequest request,
        CancellationToken cancellationToken)
    {
        var appointment = await _requestHandler.HandleAsync(new RequestAppointment(request), cancellationToken);
        return CreatedAtAction(nameof(GetUpcoming), new { id = appointment.AppointmentId }, appointment);
    }
}
