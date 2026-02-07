using DoctorAppointments.Application.Interfaces;

namespace DoctorAppointments.Api.Services;

public sealed class HttpContextUserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId =>
        _httpContextAccessor.HttpContext?.Request.Headers["X-User-Id"].ToString() ?? "anonymous";

    public string DisplayName =>
        _httpContextAccessor.HttpContext?.Request.Headers["X-User-Name"].ToString() ?? "Unknown User";

    public IReadOnlyCollection<string> Roles
    {
        get
        {
            var roles = _httpContextAccessor.HttpContext?.Request.Headers["X-User-Roles"].ToString();
            return string.IsNullOrWhiteSpace(roles)
                ? Array.Empty<string>()
                : roles.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
