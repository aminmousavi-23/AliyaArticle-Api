using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasMaxLength(64)
            .IsRequired();

        builder.HasIndex(x => x.Slug)
            .IsUnique();
        
        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}