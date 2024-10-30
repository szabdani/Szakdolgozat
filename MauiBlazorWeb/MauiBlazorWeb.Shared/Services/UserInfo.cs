using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using MauiBlazorWeb.Shared.Models;

namespace MauiBlazorWeb.Shared.Services
{
	public class UserInfo
	{
		public string Username { get; set; } = "Account";
		public string PfpPath { get; set; } = string.Empty;
		public bool isLoggedIn { get; set; } = false;

		public static async Task GetInfo(ILocalStorageService localStorage, Account userData)
		{
			await localStorage.SetItemAsync("username", userData.Username);
			await localStorage.SetItemAsync("pfppath", userData.PfpPath);
		}

		public async Task UpdateInfo(ILocalStorageService localStorage)
		{
			var user = await localStorage.GetItemAsync<string>("username") ?? "";
			if (user != "")
			{
				Username = user;
				isLoggedIn = true;
				PfpPath = await localStorage.GetItemAsync<string>("pfppath") ?? "";
			}
		}
	}
}
