namespace Lexlume.Domain.Interfaces;

public interface IDatabaseContext
{
    // entities

    // methods

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}