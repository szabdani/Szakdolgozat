namespace MauiBlazorWeb.Shared.Interfaces
{
    public interface ILocalDataStorage
	{
		Task SetItemAsync(string key, int value);
		Task<int> GetItemAsync(string key);
		Task RemoveItemAsync(string key);
	}
}