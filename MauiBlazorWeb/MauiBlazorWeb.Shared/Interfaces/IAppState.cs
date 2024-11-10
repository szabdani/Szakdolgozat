using Blazored.LocalStorage;
using MauiBlazorWeb.Shared.Components.Layout;
using MauiBlazorWeb.Shared.Models;

namespace MauiBlazorWeb.Shared.Interfaces
{
    public interface IAppState
    {
        string? Title { get; set; }
        bool IsLoggedIn { get; }
        bool IsInitialized { get; }
        Account CurrentUser { get; set; }
        List<Account> ExistingUsers { get; set; }
        MainLayout MainLayout { get; set; }

        Task<bool> Register(LoginRegUser newAccount);
		Task<bool> Delete(ILocalStorageService localStorage);
		Task Login(Account userData, ILocalStorageService localStorage);
        Task Logout(ILocalStorageService localStorage);
       
		Task UpdateExistingUsers();

        Task Init(ILocalStorageService localStorage);
    }
}