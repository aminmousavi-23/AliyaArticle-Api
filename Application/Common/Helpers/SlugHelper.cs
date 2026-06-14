using System.Text.RegularExpressions;

namespace Application.Common.Helpers;

public static class SlugHelper
{
    public static string GenerateSlug(string text)
    {
        var slug = text.Trim().ToLowerInvariant();

        // فقط حروف انگلیسی، فارسی/عربی، اعداد، فاصله و خط فاصله نگه داشته شود
        slug = Regex.Replace(slug, @"[^a-z0-9\u0600-\u06FF\s-]", "");

        // فاصله‌ها به خط فاصله تبدیل شوند
        slug = Regex.Replace(slug, @"\s+", "-");

        // چند خط فاصله پشت سر هم یکی شود
        slug = Regex.Replace(slug, @"-+", "-");

        return slug.Trim('-');
    }
}