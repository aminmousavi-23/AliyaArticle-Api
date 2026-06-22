using Application.Abstractions.Infrastructure;
using Domain.Common;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AuditLog> AuditLogs { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleBlock> ArticleBlocks { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        var userId = this.GetService<IUserContextAccessor>()
            .GetUserByTokenAsync()
            .Id;

        var now = DateTime.UtcNow;
        var auditLogs = new List<AuditLog>();

        var entries = ChangeTracker
            .Entries<AuditableEntity>()
            .Where(x => x.Entity is not AuditLog)
            .ToList();

        foreach (var entry in entries)
        {
            var originalState = entry.State;

            switch (originalState)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = userId;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = now;
                    entry.Entity.LastModifiedBy = userId;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.IsActive = false;
                    entry.Entity.LastModifiedAt = now;
                    entry.Entity.LastModifiedBy = userId;
                    break;
            }

            if (originalState is not (
                EntityState.Added or
                EntityState.Modified or
                EntityState.Deleted))
            {
                continue;
            }

            var recordId = entry.Properties
                .FirstOrDefault(x => x.Metadata.IsPrimaryKey())
                ?.CurrentValue as Guid?;

            if (recordId.HasValue == false)
                continue;

            var newValues = JsonConvert.SerializeObject(
                entry.Properties.ToDictionary(
                    p => p.Metadata.Name,
                    p => p.CurrentValue));

            auditLogs.Add(new AuditLog
            {
                TableName = entry.Metadata.ClrType.Name,
                EntityState = originalState,
                RecordId = recordId.Value,
                NewValues = newValues,
                CreatedAt = now,
                CreatedBy = userId
            });
        }

        if (auditLogs.Any())
            AuditLogs.AddRange(auditLogs);

        return await base.SaveChangesAsync(cancellationToken);
    }
}