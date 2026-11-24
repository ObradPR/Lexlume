using Lexlume.Domain.Entities.Application;
using Lexlume.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lexlume.Persistence.Contexts;

public partial class ApplicationDbContext : IDatabaseContext
{
    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }
}