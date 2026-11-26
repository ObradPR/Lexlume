using Lexlume.Common;
using Lexlume.Domain.Entities._Base;
using Lexlume.Domain.Entities._Base.Interfaces;
using Lexlume.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lexlume.Domain.Entities.Application;

public class User : BaseIndexAuditedDomain<User, long>, IConfigurableEntity
{
    public string FullName { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    [Column(TypeName = Constants.DB_TYPE_DATE)]
    public DateTime? DateOfBirth { get; set; }

    public eUserGender? GenderId { get; set; }

    public eSystemRole SystemRoleId { get; set; }

    //
    // Indexes
    //

    #region Indexes

    public static IDatabaseIndex IX_Users_Email => new DatabaseIndex(nameof(IX_Users_Email))
    {
        Columns = { nameof(Email) },
        IsUnique = true,
    };

    public static IDatabaseIndex IX_Users_Username => new DatabaseIndex(nameof(IX_Users_Username))
    {
        Columns = { nameof(Username) },
        IsUnique = true,
    };

    #endregion Indexes

    //
    // Configuration
    //

    public void Configure(ModelBuilder builder)
    {
        builder.Entity<User>(_ =>
        {
            _.Property(_ => _.FullName).HasMaxLength(User.NAME_MAX_LENGTH).IsRequired();
            _.Property(_ => _.Username).HasMaxLength(User.NAME_MAX_LENGTH).IsRequired();
            _.Property(_ => _.Email).HasMaxLength(User.EMAIL_MAX_LENGTH).IsRequired();
            _.Property(_ => _.Password).IsRequired();
        });
    }

    //
    // Constants
    //

    public const int NAME_MAX_LENGTH = 30;
    public const int EMAIL_MAX_LENGTH = 120;
}