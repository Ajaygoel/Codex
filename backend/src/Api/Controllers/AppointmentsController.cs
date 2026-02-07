using DoctorAppointments.Api.Models.Appointments;
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
    public async Task<ActionResult<IReadOnlyList<AppointmentSummaryDto>>> GetUpcoming(CancellationToken cancellationToken)
    {
        var appointments = await _getUpcomingHandler.HandleAsync(new GetUpcomingAppointments(), cancellationToken);
        var response = appointments.Select(MapAppointmentSummary).ToList();
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentSummaryDto>> RequestAppointment(
        [FromBody] AppointmentRequestDto request,
        CancellationToken cancellationToken)
    {
        var appointment = await _requestHandler.HandleAsync(new RequestAppointment(MapAppointmentRequest(request)), cancellationToken);
        var response = MapAppointmentSummary(appointment);
        return CreatedAtAction(nameof(GetUpcoming), new { id = response.AppointmentId }, response);
    }

    private static AppointmentRequest MapAppointmentRequest(AppointmentRequestDto request)
    {
        var patient = new PatientInput(request.Patient.FirstName, request.Patient.LastName, request.Patient.DateOfBirth);
        DiagnosisInput? diagnosis = request.InitialDiagnosis is null
            ? null
            : new DiagnosisInput(request.InitialDiagnosis.Summary, request.InitialDiagnosis.Notes);

        return new AppointmentRequest(patient, request.ScheduledFor, request.Reason, diagnosis);
    }

    private static AppointmentSummaryDto MapAppointmentSummary(AppointmentSummary summary) =>
        new(summary.AppointmentId,
            summary.PatientId,
            summary.PatientName,
            summary.ScheduledFor,
            summary.Reason,
            summary.Status,
            summary.InitialDiagnosisSummary);
}
