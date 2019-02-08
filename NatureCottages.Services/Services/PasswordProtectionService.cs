using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using NatureCottages.Services.Interfaces;

namespace NatureCottages.Services.Services
{
    public class PasswordProtectionService : IPasswordProtectionService
    {
        public List<byte[]> Encrypt(string password)
        {
            var salt = GenerateSalt();

            var rfc2 = new Rfc2898DeriveBytes(password, salt, 10);

            List<byte[]> details = new List<byte[]>()
            {
                rfc2.GetBytes(32),
                salt
            };

            return details;
        }

        public bool Check(string passwordEntered, byte[] passwordToCheck, byte[] salt)
        {
            var provider = new Rfc2898DeriveBytes(passwordToCheck, salt, 10);

            var one = GetString(provider.GetBytes(32));

            var two = GetString(passwordToCheck);

            return one == two;
        }

        public string GetString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        public byte[] GenerateSalt()
        {
            var salt = new byte[32];

            var provider = new RNGCryptoServiceProvider();

            provider.GetBytes(salt);

            return salt;
        }
    }
}
