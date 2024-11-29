using Blazored.LocalStorage;
using MauiBlazorWeb.Shared.Components.Layout;
using MauiBlazorWeb.Shared.Models;
using MauiBlazorWeb.Shared.Services;

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

        Task<bool> Register(LoginRegUser newAccount, IAccountManager acountManager, IPasswordHasher passwordHasher);
		Task<bool> Delete(ILocalDataStorage localStorage, IAccountManager accountManager, IDiaryManager diaryManager, ISportManager sportManager);
        Task Init(ILocalDataStorage localStorage, IAccountManager acountManager);
        Task Login(Account userData, ILocalDataStorage localStorage);
        Task Logout(ILocalDataStorage localStorage);
       
		Task UpdateExistingUsers(IAccountManager acountManager);

        Task ShowLoadingScreenWhileAwaiting(Func<Task>? action);
    }
}