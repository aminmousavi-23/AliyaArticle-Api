using Application.Abstractions.Infrastructure;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        //var userContext = this.GetService<IUserContextAccessor>();
        //var user = await userContext.GetUserByTokenAsync(); TODO:user-context-accessor

        var now = DateTime.UtcNow;
        var auditLogs = new List<AuditLog>();

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.Entity is AuditLog)
                continue;

            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = "0";
                    //entry.Entity.CreatedBy = user.Username;

                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = now;
                    entry.Entity.LastModifiedBy = "0";
                    //entry.Entity.LastModifiedBy = user.Username;

                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsActive = false;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.LastModifiedAt = now;
                    entry.Entity.LastModifiedBy = "0";
                    //entry.Entity.LastModifiedBy = user.Username;

                    break;
            }

            if (entry.State is not (EntityState.Added or EntityState.Modified))
                continue;

            var primaryKey = entry.Properties
                .FirstOrDefault(x => x.Metadata.IsPrimaryKey())
                ?.CurrentValue;

            if (primaryKey is not Guid recordId)
                continue;

            auditLogs.Add(new AuditLog
            {
                TableName = entry.Entity.GetType().Name,
                EntityState = entry.State,
                RecordId = recordId,
                NewValues = JsonConvert.SerializeObject(
                    entry.Properties.ToDictionary(
                        p => p.Metadata.Name,
                        p => p.CurrentValue)),
                CreatedAt = now,
                CreatedBy = "0"
                //CreatedBy = user.Username
            });
        }

        if (auditLogs.Any())
            AuditLogs.AddRange(auditLogs);

        return await base.SaveChangesAsync(cancellationToken);
    }
}