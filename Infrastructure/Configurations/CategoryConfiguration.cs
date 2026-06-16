using Domain.Common.Constants.ValidationConstants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(CategoryValidationConstants.NameMaxLength)
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasMaxLength(CategoryValidationConstants.SlugMaxLength)
            .IsRequired();

        builder.HasIndex(x => x.Slug)
            .IsUnique();
        
        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}