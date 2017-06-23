using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ThirdDrawer.Extensions.StringExtensionMethods
{
    public static class StringExtensions
    {
        public static string FormatWith(this string s, params object[] args)
        {
            return string.Format(CultureInfo.CurrentUICulture, s, args);
        }
        
        public static string GenerateRandomString(int length)
        {
            var characters = new List<char>();

            var random = new Random();
            for (var i = 0; i < length; i++)
            {
                var asciiCode = random.Next(33, 126); // ! to ~ in ASCII
                var c = Convert.ToChar(asciiCode);
                characters.Add(c);
            }

            return new string(characters.ToArray());
        }

        public static string HashWithSalt(this string stringToHash, string salt)
        {
            var encoding = new UTF8Encoding();
            var saltBytes = encoding.GetBytes(salt);
            var hashBytes = encoding.GetBytes(stringToHash);

            for (var i = 0; i < 50; i++)
            {
                var saltedBytesToHash = hashBytes.ToList();
                saltedBytesToHash.AddRange(saltBytes);

                using (var hash = new SHA512Managed())
                {
                    hashBytes = hash.ComputeHash(saltedBytesToHash.ToArray());
                }
            }

            return Convert.ToBase64String(hashBytes);
        }

        public static string CoalesceIfWhiteSpace(this string s, string other)
        {
            return String.IsNullOrWhiteSpace(s) ? other : s;
        }

        public static IEnumerable<string> NotNullOrWhitespace(this IEnumerable<string> source)
        {
            return source
                .Where(s => !String.IsNullOrWhiteSpace(s));
        }

        public static string Join(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source.ToArray());
        }

        public static string RegexReplace(this string s, string pattern, string replacement)
        {
            return Regex.Replace(s, pattern, replacement);
        }

        public static string RemoveCharacters(this string s, string toRemove)
        {
            var working = s;
            foreach (var c in toRemove.ToCharArray())
            {
                working = working.Replace(c.ToString(), string.Empty);
            }

            return working;
        }

        public static bool NoVal(this string src)
        {
            return string.IsNullOrWhiteSpace(src);
        }

        public static bool HasVal(this string src)
        {
            return !src.NoVal();
        }
    }
}