using MauiBlazorWeb.Shared.Models;
using MauiBlazorWeb.Shared.Models.Diaries;

namespace MauiBlazorWeb.Shared.Interfaces
{
    public interface IAccountAPIService
    {
		Task<bool> DeleteAccount(Account oldAccount);
		Task<bool> InsertAccount(Account newAccount);
		Task<bool> UpdateAccount(Account oldAccount);

		Task<List<Account>> GetAccount(int id);
		Task<List<Account>> GetAccountByUsername(string username);
		Task<List<string>> GetAllUsernames();
		Task<List<string>> GetAllEmails();
	}
}