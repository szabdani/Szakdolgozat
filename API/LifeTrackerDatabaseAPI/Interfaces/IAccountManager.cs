using LifeTrackerDatabaseAPI.Models;

namespace LifeTrackerDatabaseAPI.Intefaces
{
    public interface IAccountManager
	{
        Task<bool> DeleteAccount(int id);
		Task<bool> InsertAccount(Account newAccount);
        Task<bool> UpdateAccount(Account oldAccount);

        Task<List<Account>> GetAccount(int id);
		Task<List<Account>> GetAccountByUsername(string username);

		Task<List<string>> GetAllUsernames();
		Task<List<string>> GetAllEmails();
	}
}