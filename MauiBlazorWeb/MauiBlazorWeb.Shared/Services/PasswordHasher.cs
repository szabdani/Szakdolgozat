using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Konscious.Security.Cryptography;

namespace MauiBlazorWeb.Shared.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public byte[] GenerateSalt()
        {
            return  RandomNumberGenerator.GetBytes(16);
        }
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

        public bool VerifyPassword(byte[] passwordHash, byte[] passwordSalt, string passwordInput)
        {
            byte[] pw_hash = Hash(passwordInput, passwordSalt);

            return StructuralComparisons.StructuralEqualityComparer.Equals(passwordHash, pw_hash);
        }
    }
}
