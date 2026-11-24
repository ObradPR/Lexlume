using System.ComponentModel;

namespace Lexlume.Domain.Enums;

public enum eUserGender
{
    [Description("")]
    NotSet = 0,

    [Description("Male")]
    Male = 1,

    [Description("Female")]
    Female = 2,
}
