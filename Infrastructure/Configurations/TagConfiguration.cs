
namespace Infrastructure.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(TagValidationConstants.NameMaxLength)
            .IsRequired();

        builder.Property(x => x.Slug)
            .HasMaxLength(TagValidationConstants.SlugMaxLength)
            .IsRequired();

        builder.HasIndex(x => x.Slug)
            .IsUnique();
        
        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}