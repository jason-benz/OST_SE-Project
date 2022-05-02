using Microsoft.AspNetCore.Components.Authorization;

namespace MediaHub.Services;

public interface IIdentityService
{
    public AuthenticationState AuthenticationState { get; set; }
    public string? UserId { get; }
    public string? LoginName { get; }
    public bool IsAuthenticated { get; }
}
