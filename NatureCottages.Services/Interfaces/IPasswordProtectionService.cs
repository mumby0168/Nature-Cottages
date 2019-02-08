using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.Services.Interfaces
{
    public interface IPasswordProtectionService
    {
        List<byte[]> Encrypt(string password);

        bool Check(string passwordEntered, byte[] passwordToCheck, byte[] salt);

        string GetString(byte[] bytes);

        byte[] GenerateSalt();
    }
}
