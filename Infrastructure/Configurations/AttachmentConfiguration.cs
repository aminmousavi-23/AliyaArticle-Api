using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Size)
            .IsRequired();

        builder.Property(x => x.Data)
            .IsRequired();
        
        builder.Property(x => x.ContentType)
            .IsRequired();
    }
}