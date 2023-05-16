

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
    }
}
