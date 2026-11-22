using Lexlume.Domain.Entities._Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Lexlume.Persistence.Extensions;

internal static partial class Extensions
{
    public static void ApplyEntityConfigurations(this ModelBuilder builder, Assembly assembly)
    {
        var entityTypes = assembly.GetTypes()
            .Where(_ => typeof(IConfigurableEntity).IsAssignableFrom(_) && !_.IsAbstract)
            .ToList();

        foreach (var type in entityTypes)
        {
            var instance = Activator.CreateInstance(type) as IConfigurableEntity;
            instance?.Configure(builder);
        }
    }

    public static void ApplyGlobalSoftDeleteFilter(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(Extensions)
                    .GetMethod(nameof(ApplySoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(null, [builder]);
            }
        }
    }

    public static void ApplySoftDeleteIndexes(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                var method = typeof(Extensions)
                    .GetMethod(nameof(CreateSoftDeleteIndex), BindingFlags.NonPublic | BindingFlags.Static)
                    .MakeGenericMethod(entityType.ClrType);

                method.Invoke(null, [builder]);
            }
        }
    }

    public static void SetTableNames(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
            entityType.SetTableName(entityType.DisplayName());
    }

    // private

    private static void ApplySoftDeleteFilter<TEntity>(ModelBuilder builder)
        where TEntity : class, ISoftDelete
        => builder.Entity<TEntity>().HasQueryFilter(_ => _.IsActive == true);

    private static void CreateSoftDeleteIndex<TEntity>(ModelBuilder builder)
        where TEntity : class, ISoftDelete
        => builder.Entity<TEntity>()
            .HasIndex(_ => _.IsActive)
            .HasFilter($"[{nameof(ISoftDelete.IsActive)}] = 1");
}