using System.Security.Authentication;
using MediaHub.Data.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace MediaHub.Services;

public class IdentityService : IIdentityService
{
    public AuthenticationState _AuthenticationState { get; set; }
    
    public string? LoginName
    {
        get => _AuthenticationState.User.Identity?.Name;
    }

    public bool IsAuthenticated
    {
        get => _AuthenticationState.User.Identity.IsAuthenticated;
    }

}