using Lexlume.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Design;

namespace Lexlume.Persistence.Contexts;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        IIdentityUser identityUser = null;
        return new ApplicationDbContext(identityUser);
    }
}