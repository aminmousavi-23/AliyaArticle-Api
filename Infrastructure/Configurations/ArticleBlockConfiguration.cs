using Domain.Common.Constants.ValidationConstants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ArticleBlockConfiguration : IEntityTypeConfiguration<ArticleBlock>
{
    public void Configure(EntityTypeBuilder<ArticleBlock> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Order)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Text)
            .HasMaxLength(ArticleBlockValidationConstants.TextMaxLength);
        
        builder.HasOne(x => x.Article)
            .WithMany(x => x.Blocks)
            .HasForeignKey(x => x.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Attachment)
            .WithMany()
            .HasForeignKey(x => x.AttachmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.ArticleId, x.Order });

        builder.HasIndex(x => x.ArticleId);
    }
}