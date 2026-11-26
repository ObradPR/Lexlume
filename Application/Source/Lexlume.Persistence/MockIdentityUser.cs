using Lexlume.Common;
using Lexlume.Domain.Enums;
using Lexlume.Domain.Interfaces;

namespace Lexlume.Persistence;

public class MockIdentityUser : IIdentityUser
{
    public long Id { get; set; } = Constants.SYSTEM_USER_ID;

    public string Email { get; set; } = Constants.SYSTEM_USER_EMAIL;

    public string Username { get; set; } = Constants.SYSTEM_USER_USERNAME;

    public bool IsAuthenticated { get; set; } = true;

    public eSystemRole? RoleId => eSystemRole.Admin;

    public bool HasRole(eSystemRole role) => RoleId >= role;
}