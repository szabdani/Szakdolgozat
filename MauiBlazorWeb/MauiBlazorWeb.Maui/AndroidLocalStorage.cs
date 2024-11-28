using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Interfaces;

namespace MauiBlazorWeb.Maui
{
	public class AndroidLocalStorage : ILocalDataStorage
	{
		public Task<int> GetItemAsync(string key)
		{
			string? value = Preferences.Get(key, null);
			int retVal = value is null ? 0 : Convert.ToInt32(value);
			
			return Task.FromResult(retVal);
		}

		public Task RemoveItemAsync(string key)
		{
			Preferences.Remove(key);
			return Task.CompletedTask;
		}

		public Task SetItemAsync(string key, int value)
		{
			Preferences.Set(key, value);
			return Task.CompletedTask;
		}
	}
}
