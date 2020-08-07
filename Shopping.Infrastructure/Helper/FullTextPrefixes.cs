using System.Text.RegularExpressions;

namespace Shopping.Infrastructure.Helper
{
    public static class FullTextPrefixes
    {
        /// <summary>
        ///
        /// </summary>
        public const string ContainsPrefix = "-CONTAINS-";

        /// <summary>
        ///
        /// </summary>
        public const string FreetextPrefix = "-FREETEXT-";

        /// <summary>
        ///
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public static string ContainsWithOR(string searchTerm)
        {
            var value = Regex.Replace(searchTerm, @"\s+", " or ");
                        var s= $"({ContainsPrefix}{value})";
            return s;
        }
        public static string ContainsWithAnd(string searchTerm)
        {
            var value = Regex.Replace(searchTerm, @"\s+", " and ");
            //var valueWithQute = $"\"{value}\"";
            var s = $"({ContainsPrefix}{value})";
            return s;
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public static string Freetext(string searchTerm)
        {
            return $"({FreetextPrefix}{searchTerm})";
        }

    }
}