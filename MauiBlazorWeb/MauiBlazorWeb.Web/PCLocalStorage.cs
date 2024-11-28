using Blazored.LocalStorage;
using MauiBlazorWeb.Shared.Interfaces;


using Microsoft.AspNetCore.Components;

namespace MauiBlazorWeb.Web
{
	public class PCLocalStorage(ILocalStorageService localStorage) : ILocalDataStorage
	{
		public async Task<int> GetItemAsync(string key)
		{
			return await localStorage.GetItemAsync<int>(key);
		}

		public async Task RemoveItemAsync(string key)
		{
			await localStorage.RemoveItemAsync("id");
		}

		public async Task SetItemAsync(string key, int value)
		{
			await localStorage.SetItemAsync("id", value);
		}
	}
}
