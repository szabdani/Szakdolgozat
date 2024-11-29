using LifeTrackerDatabaseAPI.Intefaces;
using LifeTrackerDatabaseAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LifeTrackerDatabaseAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController(IAccountManager accountManager) : ControllerBase
	{
		private readonly IAccountManager _accountManager = accountManager;

		[HttpPost("InsertAccount")]
		public async Task<IActionResult> InsertAccount(Account newAccount)
		{
			var isCorrect = await _accountManager.InsertAccount(newAccount);
			return isCorrect ? Ok() : StatusCode(500, "Failed to insert Account");
		}

		[HttpPut("UpdateAccount")]
		public async Task<IActionResult> UpdateAccount(Account oldAccount)
		{
			var isCorrect = await _accountManager.UpdateAccount(oldAccount);
			return isCorrect ? Ok() : StatusCode(500, "Failed to update Account");
		}

		[HttpDelete("DeleteAccount/{id}")]
		public async Task<IActionResult> DeleteAccount(int id)
		{
			var isCorrect = await _accountManager.DeleteAccount(id);
			return isCorrect ? Ok() : StatusCode(500, "Failed to delete Account");
		}

		[HttpGet("GetAccount")]
		public async Task<IActionResult> GetAccount(int accountId)
		{
			var acc = await _accountManager.GetAccount(accountId);
			return Ok(acc);
		}

		[HttpGet("GetAllAccounts")]
		public async Task<IActionResult> GetAllAccounts()
		{
			var accs = await _accountManager.GetAllAccounts();
			return Ok(accs);
		}
	}
}
