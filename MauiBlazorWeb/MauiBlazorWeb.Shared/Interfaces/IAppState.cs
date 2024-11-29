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
		List<string> Usernames { get; set; }
		List<string> Emails { get; set; }


		Task<bool> Register(LoginRegUser newAccount, IAccountAPIService accountAPI, IPasswordHasher passwordHasher);
		Task<bool> Delete(ILocalStorageService localStorage, IAccountAPIService accountAPI, IDiaryAPIService diaryAPI, ISportAPIService sportAPI);
        Task Init(ILocalStorageService localStorage, IAccountAPIService accountAPI);
        Task<bool> Login(LoginRegUser newAccount, IAccountAPIService accountAPI, ILocalStorageService localStorage, IPasswordHasher passwordHasher);
        Task Logout(ILocalStorageService localStorage);
       
		Task UpdateExistingUsers(IAccountAPIService accountAPI);

        Task ShowLoadingScreenWhileAwaiting(Func<Task>? action);
    }
}