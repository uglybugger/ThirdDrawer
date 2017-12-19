using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ThirdDrawer.Extensions.StringExtensionMethods
{
    public static class CryptoExtensions
    {
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