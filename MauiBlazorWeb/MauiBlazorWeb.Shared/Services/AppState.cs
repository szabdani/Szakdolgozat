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

namespace MauiBlazorWeb.Shared.Services
{
    public class AppState : IAppState
    {
        public string? Title { get; set; }
        private bool isInitialized = false;
        private bool isLoggedIn = false;
        public bool IsInitialized { get { return isInitialized; } }
        public bool IsLoggedIn { get { return isLoggedIn; } }
        public bool IsLoading { get; set; } = false;
        public Account CurrentUser { get; set; }
        public List<Account> ExistingUsers { get; set; }
		

		public AppState()
        {
            ExistingUsers = new List<Account>();
            CurrentUser = new Account();
        }

        public async Task UpdateExistingUsers()
        {
            IDataAccess _data = new DataAccess();
            string sql = "select * from account";
            ExistingUsers = await _data.LoadData<Account, dynamic>(sql, new { });
        }

        public async Task Init(ILocalStorageService localStorage)
        {
            isInitialized = true;
            int id = await localStorage.GetItemAsync<int>("id");
            if (id != 0)
            {
                IDataAccess _data = new DataAccess();
                string sql = "select * from account where Id = @accountid;";
                var results = await _data.LoadData<Account, dynamic>(sql, new {accountid = id });
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
            IPasswordHasher _pw = new PasswordHasher();
            IDataAccess _data = new DataAccess();

            byte[] pw_salt = _pw.GenerateSalt();
            byte[] pw_hash = _pw.Hash(newAccount.Password1, pw_salt);

            int affectedRows;
            string date = newAccount.Birthdate.ToString("yyyy-MM-dd");
            string gender = newAccount.Gender.ToString();
            string sql = "Insert into account (username, email, password_hash, password_salt, birthdate, gender) values (@username, @email, @pw_h, @pw_s, @birthdate, @gender);";

            affectedRows = await _data.SaveData(sql, new
            {
                username = newAccount.Username,
                email = newAccount.Email,
                pw_h = pw_hash,
                pw_s = pw_salt,
                birthdate = date,
                gender = gender
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
    }
}
