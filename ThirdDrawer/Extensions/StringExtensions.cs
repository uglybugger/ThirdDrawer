using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ThirdDrawer.Extensions
{
    public static class StringExtensions
    {
        public static string FormatWith(this string s, params object[] args)
        {
            return string.Format(s, args);
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
    }
}