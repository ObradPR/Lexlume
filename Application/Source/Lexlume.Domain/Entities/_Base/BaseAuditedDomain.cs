using Lexlume.Domain.Entities._Base.Interfaces;
using Lexlume.Domain.Entities.Application;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lexlume.Domain.Entities._Base;

public class BaseAuditedDomain<TKey> : BaseDomain<TKey>, IAuditedEntity
    where TKey : struct
{
    public DateTime CreatedOn { get; set; }

    public long CreatedBy { get; set; }

    public DateTime LastChangeOn { get; set; }

    public long LastChangeBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public long? DeletedBy { get; set; }

    public bool IsActive { get; set; }

    //
    // Relationships
    //

    #region Relationships

    [ForeignKey(nameof(CreatedBy))]
    public virtual User CreatedByUser { get; set; }

    [ForeignKey(nameof(LastChangeBy))]
    public virtual User LastChangeByUser { get; set; }

    [ForeignKey(nameof(DeletedBy))]
    public virtual User DeletedByUser { get; set; }

    #endregion Relationships
}