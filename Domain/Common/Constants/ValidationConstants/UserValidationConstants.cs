namespace Domain.Common.Constants.ValidationConstants;

public static class UserValidationConstants
{
    public const int UsernameMaxLength = 64;
    public const int FullNameMaxLength = 128;
    public const int PhoneNumberMaxLength = 15;
    public const int EmailMaxLength = 256;
    public const int PasswordMinLength = 8;
    public const int PasswordMaxLength = 100;
    public const int HashedPasswordMaxLength = 256;
}