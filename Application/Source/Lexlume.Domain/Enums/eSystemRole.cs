using System.ComponentModel;

namespace Lexlume.Domain.Enums;

public enum eSystemRole
{
    [Description("Admin")]
    Admin = 1,

    [Description("Student")]
    User = 2,
}