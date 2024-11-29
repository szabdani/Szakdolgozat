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

        Task<bool> Register(LoginRegUser newAccount, IAccountAPIService accountAPI, IPasswordHasher passwordHasher);
		Task<bool> Delete(ILocalDataStorage localStorage, IAccountAPIService accountAPI, IDiaryAPIService diaryAPI, ISportAPIService sportAPI);
        Task Init(ILocalDataStorage localStorage, IAccountAPIService accountAPI);
        Task Login(Account userData, ILocalDataStorage localStorage);
        Task Logout(ILocalDataStorage localStorage);
       
		Task UpdateExistingUsers(IAccountAPIService accountAPI);

        Task ShowLoadingScreenWhileAwaiting(Func<Task>? action);
    }
}