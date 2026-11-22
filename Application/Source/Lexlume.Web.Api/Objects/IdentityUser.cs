using Lexlume.Domain.Enums;
using Lexlume.Domain.Interfaces;

namespace Lexlume.Web.Api.Objects;

public class IdentityUser(IHttpContextAccessor httpContextAccessor) : IIdentityUser
{
    public long Id => throw new NotImplementedException();

    public string Email => throw new NotImplementedException();

    public string Username => throw new NotImplementedException();

    public IEnumerable<eSystemRole> Roles => throw new NotImplementedException();

    public bool IsAuthenticated => throw new NotImplementedException();

    public bool HasRole(IEnumerable<eSystemRole> roles)
    {
        throw new NotImplementedException();
    }
}