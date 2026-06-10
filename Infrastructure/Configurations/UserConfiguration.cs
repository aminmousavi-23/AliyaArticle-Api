using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Username)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.Property(x => x.FullName)
            .HasMaxLength(128)
            .IsRequired();
        
        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasMaxLength(256)
            .IsRequired(false);
        
        builder.Property(x => x.HashedPassword)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(x => x.IsAdmin);
        
        builder.HasIndex(x => x.PhoneNumber)
            .IsUnique();
    }
}