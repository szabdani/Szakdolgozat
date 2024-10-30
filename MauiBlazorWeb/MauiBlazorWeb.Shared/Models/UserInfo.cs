using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace MauiBlazorWeb.Shared.Models
{
    public class UserInfo
    {
        public string Username { get; set; } = "Account";
        public string PfpPath { get; set; } = string.Empty;
        public bool isLoggedIn { get; set; } = false;

        public async Task UpdateInfo(ILocalStorageService localStorage)
        {
            var user = await localStorage.GetItemAsync<string>("username");
            if (user is not null)
            {
                Username = user;
                isLoggedIn = true;
                PfpPath = await localStorage.GetItemAsync<string>("pfppath");
            }
        }
    }
}
