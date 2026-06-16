namespace Application.Common.Options;

public sealed class JwtConfigOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpires { get; set; }
    public int RefreshTokenExpires { get; set; }
    public string Key { get; set; } = string.Empty;
}