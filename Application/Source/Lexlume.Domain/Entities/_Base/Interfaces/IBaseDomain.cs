namespace Lexlume.Domain.Entities._Base.Interfaces;

public interface IBaseDomain<TKey> : IEntity<TKey>
    where TKey : struct
{ }

public interface IAuditedEntity : ISoftDelete
{
    DateTime CreatedOn { get; set; }

    long CreatedBy { get; set; }

    DateTime LastChangeOn { get; set; }

    long LastChangeBy { get; set; }
}