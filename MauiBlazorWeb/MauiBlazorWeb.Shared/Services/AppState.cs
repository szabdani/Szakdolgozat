using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models;
using Blazored.LocalStorage;
using MauiBlazorWeb.Shared.Components.Layout;


namespace MauiBlazorWeb.Shared.Services
{
    public class AppState() : IAppState
    {	
		public string? Title { get; set; }
        private bool isInitialized = false;
        private bool isLoggedIn = false;

		public bool IsInitialized { get { return isInitialized; } }
        public bool IsLoggedIn { get { return isLoggedIn; } }
		public MainLayout MainLayout { get; set; } = new();

		public Account CurrentUser { get; set; } = new();
		public List<string> Usernames { get; set; } = [];
		public List<string> Emails { get; set; } = [];

		public async Task UpdateExistingUsers(IAccountAPIService accountAPI)
        {
			Usernames = await accountAPI.GetAllUsernames();
            Emails = await accountAPI.GetAllEmails();
        }

        public async Task Init(ILocalStorageService localStorage, IAccountAPIService accountAPI)
        {
            isInitialized = true;
            int id = await localStorage.GetItemAsync<int>("id");
            if (id != 0)
            {
                var results = await accountAPI.GetAccount(id);
                if (results.Count == 1)
                {
                    CurrentUser = results[0];
                    isLoggedIn = true;
                }
                else
                    throw new Exception("Account not found in database");
            }
            await UpdateExistingUsers(accountAPI);
        }

        public async Task<bool> Register(LoginRegUser newAccount, IAccountAPIService accountAPI, IPasswordHasher passwordHasher)
        {
            byte[] pw_salt = passwordHasher.GenerateSalt();
            byte[] pw_hash = passwordHasher.Hash(newAccount.Password1, pw_salt);

            var insert = new Account{ 
                Username = newAccount.Username,
                Email = newAccount.Email,
                Password_hash = pw_hash,
                Password_salt = pw_salt,
                Birthdate = newAccount.Birthdate,
                Gender = newAccount.Gender,
                RegistrationDate = DateTime.Now
            };

            bool retVal = await accountAPI.InsertAccount(insert);
            await UpdateExistingUsers(accountAPI);
            return retVal;
        }

        public async Task<bool> Login(LoginRegUser newAccount, IAccountAPIService accountAPI, ILocalStorageService localStorage, IPasswordHasher passwordHasher)
        {
            var userData = (await accountAPI.GetAccountByUsername(newAccount.LoginUsername)).First();
            if (userData is null || userData.Password_hash is null || userData.Password_salt is null)
                return false;

            if (passwordHasher.VerifyPassword(userData.Password_hash, userData.Password_salt, newAccount.LoginPassword))
            {
				CurrentUser = userData;
				isLoggedIn = true;
				await localStorage.SetItemAsync("id", userData.Id);
				return true;
			}
            else
                return false;
        }

        public async Task Logout(ILocalStorageService localStorage)
        {
            CurrentUser = new Account();
            isLoggedIn = false;
            await localStorage.RemoveItemAsync("id");
        }

        public async Task<bool> Delete(ILocalStorageService localStorage, IAccountAPIService accountAPI, IDiaryAPIService diaryAPI, ISportAPIService sportAPI)
        {
            bool isCorrect = true;

			var cols = await diaryAPI.GetDiaryCols(CurrentUser.Id, true);
            cols.AddRange(await diaryAPI.GetDiaryCols(CurrentUser.Id, false));

            foreach (var col in cols)
            {
                isCorrect = await diaryAPI.DeleteDiaryCol(col);
                if (!isCorrect)
                    return false;
            }

            var accountDoesSports = await sportAPI.GetAccountDoesSports(CurrentUser.Id);
            foreach (var accountDoesSport in accountDoesSports)
            {
				isCorrect = await sportAPI.DeleteAccountDoesSport(accountDoesSport);
				if (!isCorrect)
					return false;
			}

            isCorrect = await accountAPI.DeleteAccount(CurrentUser);

            if (!isCorrect)
                return false;

			await UpdateExistingUsers(accountAPI);
			await Logout(localStorage);

            return true;
        }

        public async Task ShowLoadingScreenWhileAwaiting(Func<Task>? action)
        {
            if (action is null)
                return;

            await MainLayout.SetLoadingScreen(true);
            await action();
            await MainLayout.SetLoadingScreen(false);
        }
    }
}
