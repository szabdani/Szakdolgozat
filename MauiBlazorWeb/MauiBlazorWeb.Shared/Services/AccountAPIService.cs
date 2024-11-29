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
    public class AccountAPIService : IAccountAPIService
	{
		public AccountAPIService(HttpClient httpClientIn)
		{
			httpClient = httpClientIn;
		}

		private HttpClient httpClient;
		public async Task<bool> InsertAccount(Account newAccount)
		{
			var response = await httpClient.PostAsJsonAsync("api/Account/InsertAccount", newAccount);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateAccount(Account oldAccount)
		{
			var response = await httpClient.PutAsJsonAsync("api/Account/UpdateAccount", oldAccount);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> DeleteAccount(Account oldAccount)
		{
			var response = await httpClient.DeleteAsync($"api/Account/DeleteAccount/{oldAccount.Id}");
			return response.IsSuccessStatusCode;
		}
		public async Task<List<Account>> GetAccount(int accountId)
		{
			var response = await httpClient.GetAsync($"api/Account/GetAccount?accountId={accountId}");
			return await response.Content.ReadFromJsonAsync<List<Account>>() ?? [];
		}
		public async Task<List<Account>> GetAccountByUsername(string username)
		{
			var response = await httpClient.GetAsync($"api/Account/GetAccountByUsername?username={username}");
			return await response.Content.ReadFromJsonAsync<List<Account>>() ?? [];
		}
		public async Task<List<string>> GetAllUsernames()
		{
			var response = await httpClient.GetAsync($"api/Account/GetAllUsernames");
			return await response.Content.ReadFromJsonAsync<List<string>>() ?? [];
		}

		public async Task<List<string>> GetAllEmails()
		{
			var response = await httpClient.GetAsync($"api/Account/GetAllEmails");
			return await response.Content.ReadFromJsonAsync<List<string>>() ?? [];
		}
	}
}
