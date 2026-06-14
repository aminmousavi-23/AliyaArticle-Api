using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AuthorName)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.AuthorEmail)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(x => x.Content)
            .HasMaxLength(2048)
            .IsRequired();

        builder.HasOne(x => x.Article)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}