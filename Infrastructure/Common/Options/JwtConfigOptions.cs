namespace Infrastructure.Common.Options;

public sealed class JwtConfigOptions
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int Expires { get; set; }
    public string Key { get; set; } = string.Empty;
}