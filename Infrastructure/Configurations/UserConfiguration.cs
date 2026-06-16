using Domain.Common.Constants.ValidationConstants;
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
            .HasMaxLength(UserValidationConstants.UsernameMaxLength)
            .IsRequired();
        
        builder.Property(x => x.FullName)
            .HasMaxLength(UserValidationConstants.FullNameMaxLength)
            .IsRequired();
        
        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(UserValidationConstants.PhoneNumberMaxLength)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasMaxLength(UserValidationConstants.EmailMaxLength)
            .IsRequired(false);
        
        builder.Property(x => x.HashedPassword)
            .HasMaxLength(UserValidationConstants.PasswordMaxLength)
            .IsRequired();
        
        builder.Property(x => x.IsAdmin);
        
        builder.HasIndex(x => x.PhoneNumber)
            .IsUnique();
        
        builder.HasQueryFilter(x => x.IsDeleted == false);
    }
}