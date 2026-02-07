using DoctorAppointments.Api.Authentication;
using DoctorAppointments.Api.Services;
using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Application.Services;
using DoctorAppointments.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IAppointmentRepository, InMemoryAppointmentRepository>();
builder.Services.AddSingleton<ITenantRepository, InMemoryTenantRepository>();
builder.Services.AddSingleton<TenantService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
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
