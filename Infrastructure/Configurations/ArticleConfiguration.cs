using Domain.Common.Constants.ValidationConstants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(ArticleValidationConstants.TitleMaxLength)
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasMaxLength(ArticleValidationConstants.SlugMaxLength)
            .IsRequired();

        builder.Property(x => x.Summary)
            .HasMaxLength(ArticleValidationConstants.SummaryMaxLength)
            .IsRequired();

        builder.Property(x => x.IsPublished)
            .HasDefaultValue(false);

        builder.HasIndex(x => x.Slug)
            .IsUnique();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Articles)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Tags)
            .WithMany(x => x.Articles)
            .UsingEntity(j => j.ToTable("ArticleTags"));
        
        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}