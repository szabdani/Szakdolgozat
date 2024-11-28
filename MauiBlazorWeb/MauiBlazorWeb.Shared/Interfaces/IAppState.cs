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

        MainLayout MainLayout { get; set; }

        Account CurrentUser { get; set; }
        List<Account> ExistingUsers { get; set; }

        Task<bool> Register(LoginRegUser newAccount);
		Task<bool> Delete(ILocalDataStorage localStorage);
        Task Init(ILocalDataStorage localStorage);
        Task Login(Account userData, ILocalDataStorage localStorage);
        Task Logout(ILocalDataStorage localStorage);
       
		Task UpdateExistingUsers();

        Task ShowLoadingScreenWhileAwaiting(Func<Task>? action);
    }
}