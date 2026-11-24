using Lexlume.Domain.Entities.Application;
using Microsoft.EntityFrameworkCore;

namespace Lexlume.Domain.Interfaces;

public interface IDatabaseContext
{
    // entities

    DbSet<ErrorLog> ErrorLogs { get; set; }

    DbSet<User> Users { get; set; }

    // methods

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}