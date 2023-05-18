

using System;
using System.Linq;

namespace Leonardo
{
    internal static class Utilities
    {
        internal static bool ValidateExtension(string extension)
        {
            string[] acceptedExtensions = { "png", "jpg", "jpeg", "webp" };
            return acceptedExtensions.Contains(extension);
        }

        internal static string TrimPeriodFromExtensions(string extension)
        {
            if (extension.StartsWith("."))
            {
                extension = extension.TrimStart('.');
            }

            return extension;
        }

        internal static bool CheckIfImageFormat(string filename)
        {
            return filename.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                   filename.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                   filename.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                   filename.EndsWith(".webp", StringComparison.OrdinalIgnoreCase);
        }

        internal static string GenerateGuidString()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
