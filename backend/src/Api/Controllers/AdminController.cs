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
    public async Task<ActionResult<IReadOnlyList<Tenant>>> GetTenants(CancellationToken cancellationToken)
    {
        var tenants = await _getTenantsHandler.HandleAsync(new GetTenants(), cancellationToken);
        return Ok(tenants);
    }
}
