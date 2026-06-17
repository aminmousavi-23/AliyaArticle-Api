namespace Application.Common.Helpers;

public static class AttachmentHelper
{
    public static string DetectContentType(byte[] bytes)
    {
        return bytes.Length switch
        {
            < 4 => "application/octet-stream",
            // PNG
            > 8 when bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47 => "image/png",
            // JPEG / JPG
            > 3 when bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF => "image/jpeg",
            // PDF
            > 4 when bytes[0] == 0x25 && bytes[1] == 0x50 && bytes[2] == 0x44 && bytes[3] == 0x46 => "application/pdf",
            _ => "application/octet-stream"
        };
    }

    public static bool IsAllowedType(byte[] bytes, string[] allowedTypes)
    {
        var contentType = DetectContentType(bytes);
        return allowedTypes.Contains(contentType);
    }
    public static long GetSize(byte[] bytes)
    {
        return bytes?.LongLength ?? 0;
    }
}