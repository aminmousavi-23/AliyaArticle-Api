using System.Security.Cryptography;
using System.Text;

namespace Application.Common.Helpers;

public static class CredentialHelper
{
    public static string HashSha256(string password)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}