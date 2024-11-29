using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Components.Pages;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models;
using MauiBlazorWeb.Shared.Models.Diaries;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MauiBlazorWeb.Shared.Services
{
    public class AccountManager(HttpClient httpClient) : IAccountManager
	{
		public async Task<bool> InsertAccount(Account newAccount)
		{
			var response = await httpClient.PostAsJsonAsync("api/Diary/InsertAccount", newAccount);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateAccount(Account oldAccount)
		{
			var response = await httpClient.PutAsJsonAsync("api/Diary/UpdateAccount", oldAccount);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> DeleteAccount(Account oldAccount)
		{
			var response = await httpClient.DeleteAsync($"api/Diary/DeleteAccount/{oldAccount.Id}");
			return response.IsSuccessStatusCode;
		}
		public async Task<List<Account>> GetAccount(int accountId)
		{
			var response = await httpClient.GetAsync($"api/Diary/GetAccount?accountId={accountId}");
			return await response.Content.ReadFromJsonAsync<List<Account>>() ?? [];
		}
		public async Task<List<Account>> GetAllAccounts()
		{
			var response = await httpClient.GetAsync($"api/Diary/GetAllAccounts");
			return await response.Content.ReadFromJsonAsync<List<Account>>() ?? [];
		}
	}
}
