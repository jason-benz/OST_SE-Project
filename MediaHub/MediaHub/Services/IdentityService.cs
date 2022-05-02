using Microsoft.AspNetCore.Components.Authorization;

namespace MediaHub.Services;

public class IdentityService : IIdentityService
{
    public AuthenticationState AuthenticationState { get; set; }
    
    public string? LoginName
    {
        get => AuthenticationState.User.Identity?.Name;
    }

    public bool IsAuthenticated
    {
        get => AuthenticationState.User.Identity != null && AuthenticationState.User.Identity.IsAuthenticated;
    }

    public string? UserId
    {
        get => AuthenticationState.User.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;
    }
}