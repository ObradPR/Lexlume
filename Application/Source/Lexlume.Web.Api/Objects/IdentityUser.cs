using Lexlume.Domain.Enums;
using Lexlume.Domain.Interfaces;
using System.Security.Claims;

namespace Lexlume.Web.Api.Objects;

public class IdentityUser(IHttpContextAccessor httpContextAccessor) : IIdentityUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    private bool _isParsed;
    private long _id;
    private string _email = string.Empty;
    private string _username = string.Empty;
    private eSystemRole _role;
    private bool _isAuthenticated;

    public long Id
    {
        get
        {
            ParseIdentity();
            return _id;
        }
    }

    public string Email
    {
        get
        {
            ParseIdentity();
            return _email;
        }
    }

    public string Username
    {
        get
        {
            ParseIdentity();
            return _username;
        }
    }

    public eSystemRole? RoleId
    {
        get
        {
            ParseIdentity();
            return _isAuthenticated ? _role : null;
        }
    }

    public bool IsAuthenticated
    {
        get
        {
            ParseIdentity();
            return _isAuthenticated;
        }
    }

    public bool HasRole(eSystemRole role)
    {
        ParseIdentity();

        return _isAuthenticated && _role >= role;
    }

    private void ParseIdentity()
    {
        if (_isParsed)
            return;

        _isParsed = true;

        var user = _httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
            return;

        var idValue = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var roleValue = user.FindFirst(ClaimTypes.Role)?.Value;

        if (!long.TryParse(idValue, out _id) ||
            !Enum.TryParse(roleValue, true, out _role) ||
            !Enum.IsDefined(_role))
        {
            return;
        }

        _email = user.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        _username = user.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;

        _isAuthenticated = true;
    }
}