using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;

namespace MauiBlazorWeb.Shared.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public byte[] Hash(string password, byte[] salt)
        {
            using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password)))
            {
                hasher.Salt = salt;
                hasher.DegreeOfParallelism = 2;
                hasher.MemorySize = 65536;
                hasher.Iterations = 4;
                return hasher.GetBytes(32);
            }
        }

        public bool VerifyPassword(string passwordHash, string passwordInput)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(passwordHash, passwordInput);
        }
    }
}
