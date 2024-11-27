using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models;
using Org.BouncyCastle.Math.EC;
using Blazored.LocalStorage;
using MauiBlazorWeb.Shared.Components.Layout;
using Microsoft.AspNetCore.Components;

namespace MauiBlazorWeb.Shared.Services
{
    public class AppState(IDataAccess data) : IAppState
    {
		[Inject] public required IDataAccess Data { get; set; } = data;
		[Inject] public required IPasswordHasher Pw { get; set; }
		[Inject] public required IDiaryManager DiaryManager { get; set; }
		[Inject] public required ISportManager SportManager { get; set; }

		public string? Title { get; set; }
        private bool isInitialized = false;
        private bool isLoggedIn = false;

		public bool IsInitialized { get { return isInitialized; } }
        public bool IsLoggedIn { get { return isLoggedIn; } }
		public MainLayout MainLayout { get; set; } = new();

		public Account CurrentUser { get; set; } = new();
		public List<Account> ExistingUsers { get; set; } = [];

		public async Task UpdateExistingUsers()
        {
            string sql = "Select * from account";
            ExistingUsers = await Data.LoadData<Account, dynamic>(sql, new { });
        }

        public async Task Init(ILocalStorageService localStorage)
        {
            isInitialized = true;
            int id = await localStorage.GetItemAsync<int>("id");
            if (id != 0)
            {
                string sql = "Select * from account where Id = @accountid;";
                var results = await Data.LoadData<Account, dynamic>(sql, new {accountid = id });
                if (results.Count == 1)
                {
                    CurrentUser = results[0];
                    isLoggedIn = true;
                }
                else
                    throw new Exception("Account not found in database");
            }
            await UpdateExistingUsers();
        }

        public async Task<bool> Register(LoginRegUser newAccount)
        {
            byte[] pw_salt = Pw.GenerateSalt();
            byte[] pw_hash = Pw.Hash(newAccount.Password1, pw_salt);

            int affectedRows;
            string date = newAccount.Birthdate.ToString("yyyy-MM-dd");
            string rDate = DateTime.Now.ToString("yyyy-MM-dd");
            string gen = newAccount.Gender.ToString();
            string sql = "Insert into account (username, email, password_hash, password_salt, birthdate, gender, registrationdate) values (@username, @email, @pw_h, @pw_s, @birthdate, @gender, @regdate);";

            affectedRows = await Data.SaveData(sql, new
            {
                username = newAccount.Username,
                email = newAccount.Email,
                pw_h = pw_hash,
                pw_s = pw_salt,
                birthdate = date,
                gender = gen,
                regdate = rDate
            }
            );

            await UpdateExistingUsers();
            return affectedRows != 0;
        }

        public async Task Login(Account userData, ILocalStorageService localStorage)
        {
            CurrentUser = userData;
            isLoggedIn = true;
            await localStorage.SetItemAsync("id", userData.Id);
        }

        public async Task Logout(ILocalStorageService localStorage)
        {
            CurrentUser = new Account();
            isLoggedIn = false;
            await localStorage.RemoveItemAsync("id");
        }

        public async Task<bool> Delete(ILocalStorageService localStorage)
        {
            var cols = await DiaryManager.GetDiaryCols(CurrentUser.Id, true);
            cols.AddRange(await DiaryManager.GetDiaryCols(CurrentUser.Id, false));

            foreach (var col in cols)
            {
                bool isCorrect = await DiaryManager.DeleteDiaryCol(col);
                if (!isCorrect)
                    return false;
            }

            var accountDoesSports = await SportManager.GetAccountDoesSports(CurrentUser.Id);
            foreach (var accountDoesSport in accountDoesSports)
            {
				bool isCorrect = await SportManager.DeleteAccountDoesSport(accountDoesSport);
				if (!isCorrect)
					return false;
			}

            string sql = "Delete from account where id = @accid;";
            int affectedRows = await Data.SaveData(sql, new { accid = CurrentUser.Id });
            if (affectedRows == 0)
                return false;

            await Logout(localStorage);

            return true;
        }

        public async Task ShowLoadingScreenWhileAwaiting(Func<Task>? action)
        {
            await MainLayout.SetLoadingScreen(true);

            try
            {
                if (action is not null)
                    await action();
            }
            finally
            {
                await MainLayout.SetLoadingScreen(false);
            }
        }
    }
}
