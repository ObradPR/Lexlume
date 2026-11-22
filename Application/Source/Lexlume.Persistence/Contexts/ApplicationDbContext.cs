using Lexlume.Common;
using Lexlume.Common.Extensions;
using Lexlume.Domain.Entities._Base;
using Lexlume.Domain.Entities._Base.Interfaces;
using Lexlume.Domain.Interfaces;
using Lexlume.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Lexlume.Persistence.Contexts;

public class ApplicationDbContext : DbContext, IDatabaseContext
{
    public IIdentityUser CurrentUser { get; private set; }

    public ApplicationDbContext(IIdentityUser identityUser) => CurrentUser = identityUser;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer(Settings.ConnectionString)
                .LogTo(
                    _ => Debug.WriteLine(_),
                    LogLevel.Information
                );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Constants.DEFAULT_DB_SCHEMA);

        modelBuilder.ApplyEntityConfigurations(typeof(BaseDomain).Assembly);
        modelBuilder.ApplyGlobalSoftDeleteFilter();
        modelBuilder.ApplySoftDeleteIndexes();
        modelBuilder.SetTableNames();

        base.OnModelCreating(modelBuilder);
    }

    // methods

    public bool HasChanges() => ChangeTracker.HasChanges();

    public void ClearChanges() => ChangeTracker.Clear();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (HasChanges())
        {
            var (now, userId) = GetAuditInfo();

            CreateAndUpdateEntries(now, userId, cancellationToken);
            SoftDeleteEntries(now, userId, cancellationToken);
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    // private

    private void CreateAndUpdateEntries(DateTime now, long userId, CancellationToken cancellationToken = default)
    {
        var newEntries = ChangeTracker.Entries<IAuditedEntity>()
            .Where(_ => _.State.In(EntityState.Modified, EntityState.Added));

        foreach (var entityEntry in newEntries)
        {
            var entity = entityEntry.Entity;

            var lastChangeByProperty = entityEntry.Property(_ => _.LastChangeBy);

            if (lastChangeByProperty.IsModified)
                entity.LastChangeBy = userId;

            entity.LastChangeOn = now;

            if (entityEntry.State == EntityState.Added)
            {
                entity.IsActive = true;
                entity.CreatedOn = now;
                entity.CreatedBy = userId;
            }
        }

        foreach (var item in ChangeTracker.Entries<ILoggerEntity>().Where(_ => _.State == EntityState.Added))
            item.Entity.RecordDate = now;
    }

    private void SoftDeleteEntries(DateTime now, long userId, CancellationToken cancellationToken = default)
    {
        var softDeleteEntries = ChangeTracker.Entries<ISoftDelete>()
            .Where(_ => _.State == EntityState.Deleted);

        foreach (var entityEntry in softDeleteEntries)
        {
            entityEntry.State = EntityState.Unchanged;

            entityEntry.Property(nameof(ISoftDelete.IsActive)).IsModified = true;
            entityEntry.Property(nameof(ISoftDelete.DeletedOn)).IsModified = true;
            entityEntry.Property(nameof(ISoftDelete.DeletedBy)).IsModified = true;

            var entity = entityEntry.Entity;

            entity.IsActive = false;
            entity.DeletedOn = now;
            entity.DeletedBy = userId;
        }
    }

    private (DateTime now, long userId) GetAuditInfo()
    {
        var now = DateTime.UtcNow;

        var userId = CurrentUser?.Id > 0
            ? CurrentUser.Id
            : Constants.SYSTEM_USER_ID;

        return (now, userId);
    }
}