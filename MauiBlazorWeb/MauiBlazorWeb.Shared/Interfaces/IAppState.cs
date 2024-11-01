using Blazored.LocalStorage;
using MauiBlazorWeb.Shared.Models;

namespace MauiBlazorWeb.Shared.Interfaces
{
    public interface IAppState
    {
        List<Account> ExistingUsers { get; set; }
        bool IsLoading { get; set; }
        bool IsLoggedIn { get; set; }
        bool IsInitialized { get; set; }
        Account CurrentUser { get; set; }
        string? Title { get; set; }

        Task<bool> Register(LoginRegUser newAccount);
        Task Login(Account userData, ILocalStorageService localStorage);
        Task Logout(ILocalStorageService localStorage);
        Task UpdateExistingUsers();

        Task Init(ILocalStorageService localStorage);
    }
}