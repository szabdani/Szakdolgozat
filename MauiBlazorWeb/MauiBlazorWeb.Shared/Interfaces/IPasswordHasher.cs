namespace MauiBlazorWeb.Shared.Interfaces
{
    public interface IPasswordHasher
    {
        byte[] GenerateSalt();
        byte[] Hash(string password, byte[] salt);
        bool VerifyPassword(byte[] passwordHash, byte[] passwordSalt, string passwordInput);
    }
}