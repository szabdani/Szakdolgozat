using LifeTrackerDatabaseAPI.Intefaces;
using LifeTrackerDatabaseAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LifeTrackerDatabaseAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DiaryController(IDiaryManager diaryManager) : ControllerBase
	{
		private readonly IDiaryManager _diaryManager = diaryManager;

		[HttpPost("InsertAccount")]
		public async Task<IActionResult> InsertAccount(Account newAccount)
		{
			var isCorrect = await _diaryManager.InsertAccount(newAccount);
			return isCorrect ? Ok() : StatusCode(500, "Failed to insert Account");
		}

		[HttpPut("UpdateAccount")]
		public async Task<IActionResult> UpdateAccount(Account oldAccount)
		{
			var isCorrect = await _diaryManager.UpdateAccount(oldAccount);
			return isCorrect ? Ok() : StatusCode(500, "Failed to update Account");
		}

		[HttpDelete("DeleteAccount/{id}")]
		public async Task<IActionResult> DeleteAccount(int id)
		{
			var isCorrect = await _diaryManager.DeleteAccount(id);
			return isCorrect ? Ok() : StatusCode(500, "Failed to delete Account");
		}

		[HttpGet("GetAccount")]
		public async Task<IActionResult> GetAccount(int accountId)
		{
			var acc = await _diaryManager.GetAccount(accountId);
			return Ok(acc);
		}

		[HttpGet("GetAllAccounts")]
		public async Task<IActionResult> GetAllAccounts()
		{
			var accs = await _diaryManager.GetAllAccounts();
			return Ok(accs);
		}

		[HttpPost("InsertDiaryCol")]
		public async Task<IActionResult> InsertDiaryCol(Diary_log_column newCol)
		{
			var isCorrect = await _diaryManager.InsertDiaryCol(newCol);
			return isCorrect ? Ok() : StatusCode(500, "Failed to insert Diary column");
		}

		[HttpPut("UpdateDiaryCol")]
		public async Task<IActionResult> UpdateDiaryCol(Diary_log_column oldCol)
		{
			var isCorrect = await _diaryManager.UpdateDiaryCol(oldCol);
			return isCorrect ? Ok() : StatusCode(500, "Failed to update Diary column");
		}

		[HttpDelete("DeleteDiaryCol/{id}")]
		public async Task<IActionResult> DeleteDiaryCol(int id)
		{
			var isCorrect = await _diaryManager.DeleteDiaryCol(id);
			return isCorrect ? Ok() : StatusCode(500, "Failed to delete Diary column");
		}

		[HttpPost("InsertDiaryPost")]
		public async Task<IActionResult> InsertDiaryPost(Diary_log_post newPost)
		{
			var isCorrect = await _diaryManager.InsertDiaryPost(newPost);
			return isCorrect ? Ok() : StatusCode(500, "Failed to insert Diary post");
		}

		[HttpPut("UpdateDiaryPost")]
		public async Task<IActionResult> UpdateDiaryPost(Diary_log_post oldPost)
		{
			var isCorrect = await _diaryManager.UpdateDiaryPost(oldPost);
			return isCorrect ? Ok() : StatusCode(500, "Failed to update Diary post");
		}

		[HttpDelete("DeleteDiaryPost/{id}")]
		public async Task<IActionResult> DeleteDiaryPost(int id)
		{
			var isCorrect = await _diaryManager.DeleteDiaryPost(id);
			return isCorrect ? Ok() : StatusCode(500, "Failed to delete Diary post");
		}

		[HttpGet("GetDiaryCols")]
		public async Task<IActionResult> GetDiaryCols(int accountId, bool isHabit)
		{
			var cols = await _diaryManager.GetDiaryCols(accountId, isHabit);
			return Ok(cols);
		}

		[HttpGet("GetDiaryColumnsPosts")]
		public async Task<IActionResult> GetDiaryColumnsPosts(int columnId)
		{
			var posts = await _diaryManager.GetDiaryColumnsPosts(columnId);
			return Ok(posts);
		}

		[HttpGet("GetDiaryPosts")]
		public async Task<IActionResult> GetDiaryPosts(int accountId, bool isHabit)
		{
			var posts = await _diaryManager.GetDiaryPosts(accountId, isHabit);
			return Ok(posts);
		}
	}
}
