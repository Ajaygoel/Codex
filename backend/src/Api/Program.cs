using DoctorAppointments.Api.Authentication;
using DoctorAppointments.Api.Services;
using DoctorAppointments.Application.Commands;
using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Application.Queries;
using DoctorAppointments.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Doctor Appointments API",
        Version = "v1"
    });
    options.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
});

builder.Services.AddSingleton<IAppointmentRepository, InMemoryAppointmentRepository>();
builder.Services.AddSingleton<ITenantRepository, InMemoryTenantRepository>();
builder.Services.AddScoped<ICommandHandler<RequestAppointment, Application.Models.AppointmentSummary>, RequestAppointmentHandler>();
builder.Services.AddScoped<IQueryHandler<GetUpcomingAppointments, IReadOnlyList<Application.Models.AppointmentSummary>>, GetUpcomingAppointmentsHandler>();
builder.Services.AddScoped<IQueryHandler<GetTenants, IReadOnlyList<Domain.Entities.Tenant>>, GetTenantsHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ITenantProvider, HttpContextTenantProvider>();
builder.Services.AddScoped<IUserContext, HttpContextUserContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<SsoAuthenticationMiddleware>();
app.MapControllers();

app.Run();
