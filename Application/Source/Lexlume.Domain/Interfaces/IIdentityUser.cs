using Lexlume.Domain.Enums;

namespace Lexlume.Domain.Interfaces;

public interface IIdentityUser
{
    long Id { get; }

    string Email { get; }

    string Username { get; }

    eSystemRole? RoleId { get; }

    bool IsAuthenticated { get; }

    bool HasRole(eSystemRole role);
}