using System.Globalization;

namespace Server.Extensions
{
    public static class StringExtension
    {
        private const string Separator = " ";
        private static readonly char[] separator = new[] { ' ' };

        public static string SanitizeNames(this string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
            {
                return string.Empty;
            }

            var trimmedName = string.Join(Separator, fullName.Split(separator, StringSplitOptions.RemoveEmptyEntries));
            var capitalizedName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(trimmedName.ToLower());

            return capitalizedName;
        }
    }
}
