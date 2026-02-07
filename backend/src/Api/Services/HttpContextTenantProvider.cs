using DoctorAppointments.Application.Interfaces;

namespace DoctorAppointments.Api.Services;

public sealed class HttpContextTenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetTenantId()
    {
        var tenantId = _httpContextAccessor.HttpContext?.Request.Headers["X-Tenant-Id"].ToString();
        return string.IsNullOrWhiteSpace(tenantId) ? "north-clinic" : tenantId;
    }
}
