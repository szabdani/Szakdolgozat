using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using LifeTrackerDatabaseAPI.Intefaces;
using LifeTrackerDatabaseAPI.Models;
using Microsoft.AspNetCore.Components;

namespace LifeTrackerDatabaseAPI.Services
{
    public class AccountManager(IDataAccess data) : IAccountManager
	{
		[Inject] protected IDataAccess Data { get; set; } = data;

		public async Task<bool> InsertAccount(Account newAccount)
		{
			string sql = "Insert into account (username, email, password_hash, password_salt, birthdate, gender, registrationdate) values (@username, @email, @pw_h, @pw_s, @birthdate, @gender, @regdate);";
			int affectedRows = await Data.SaveData(sql, new { username = newAccount.Username, email = newAccount.Email, pw_h = newAccount.Password_hash, pw_s = newAccount.Password_salt, birthdate = newAccount.Birthdate.ToString("yyyy-MM-dd"), gender = newAccount.Gender.ToString(), regdate = newAccount.RegistrationDate.ToString("yyyy-MM-dd") });
			return affectedRows != 0;
		}
		public async Task<bool> UpdateAccount(Account oldAccount)
		{
			string sql = "Update account set username = @username, email = @email, password_hash = @pw_h, password_salt = @pw_s, birthdate = @birthdate, gender = @gender, registrationdate = @regdate where id = @id;";
			int affectedRows = await Data.SaveData(sql, new { username = oldAccount.Username, email = oldAccount.Email, pw_h = oldAccount.Password_hash, pw_s = oldAccount.Password_salt, birthdate = oldAccount.Birthdate.ToString("yyyy-MM-dd"), gender = oldAccount.Gender.ToString(), regdate = oldAccount.RegistrationDate.ToString("yyyy-MM-dd"), id = oldAccount.Id });
			return affectedRows != 0;
		}
		public async Task<bool> DeleteAccount(int id)
		{
			string sql = "Delete from account where id = @accid;";
			int affectedRows = await Data.SaveData(sql, new { accid = id });
			return affectedRows != 0;
		}
		public async Task<List<Account>> GetAccount(int accountId)
		{
			string sql = "Select * from account where Id = @accid;";
			return await Data.LoadData<Account, dynamic>(sql, new {accid = accountId });
		}
		public async Task<List<Account>> GetAllAccounts()
		{
			string sql = "Select * from account;";
			return await Data.LoadData<Account, dynamic>(sql, new {});
		}
	}
}
