using System.Collections;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace MauiBlazorWeb.Shared.Interfaces
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
