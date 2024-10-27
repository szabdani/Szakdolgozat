namespace MauiBlazorWeb.Shared.Services
{
    public interface IPasswordHasher
    {
        byte[] Hash(string password, byte[] salt);
        bool VerifyPassword(string passwordHash, string passwordInput);
    }
}