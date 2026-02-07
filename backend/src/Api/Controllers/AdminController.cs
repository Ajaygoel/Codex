using DoctorAppointments.Api.Models.Admin;
using DoctorAppointments.Application.Interfaces;
using DoctorAppointments.Application.Queries;
using DoctorAppointments.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointments.Api.Controllers;

[ApiController]
[Route("api/admin")]
public sealed class AdminController : ControllerBase
{
    private readonly IQueryHandler<GetTenants, IReadOnlyList<Tenant>> _getTenantsHandler;

    public AdminController(IQueryHandler<GetTenants, IReadOnlyList<Tenant>> getTenantsHandler)
    {
        _getTenantsHandler = getTenantsHandler;
    }

    [HttpGet("tenants")]
    public async Task<ActionResult<IReadOnlyList<TenantDto>>> GetTenants(CancellationToken cancellationToken)
    {
        var tenants = await _getTenantsHandler.HandleAsync(new GetTenants(), cancellationToken);
        var response = tenants.Select(MapTenant).ToList();
        return Ok(response);
    }

    private static TenantDto MapTenant(Tenant tenant) => new(tenant.Id, tenant.Name, tenant.Region);
}
