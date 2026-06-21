
namespace Infrastructure.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.AuthorName)
            .HasMaxLength(CommentValidationConstants.AuthorNameMaxLength)
            .IsRequired();

        builder.Property(x => x.AuthorEmail)
            .HasMaxLength(CommentValidationConstants.AuthorEmailMaxLength)
            .IsRequired();

        builder.Property(x => x.Content)
            .HasMaxLength(CommentValidationConstants.ContentMaxLength)
            .IsRequired();

        builder.HasOne(x => x.Article)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}