using DoctorAppointments.Application.Models;

namespace DoctorAppointments.Application.Commands;

public sealed record RequestAppointment(AppointmentRequest Request);
