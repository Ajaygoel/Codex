using DoctorAppointments.Application.Services;
using DoctorAppointments.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointments.Api.Controllers;

[ApiController]
[Route("api/admin")]
public sealed class AdminController : ControllerBase
{
    private readonly TenantService _tenantService;

    public AdminController(TenantService tenantService)
    {
        _tenantService = tenantService;
    }

    [HttpGet("tenants")]
    public async Task<ActionResult<IReadOnlyList<Tenant>>> GetTenants(CancellationToken cancellationToken)
    {
        var tenants = await _tenantService.GetAllAsync(cancellationToken);
        return Ok(tenants);
    }
}
