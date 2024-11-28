using LifeTrackerDatabaseAPI.Intefaces;
using LifeTrackerDatabaseAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LifeTrackerDatabaseAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SportController : ControllerBase
	{
		private readonly ISportManager _sportManager;
		public SportController(ISportManager sportManager)
		{
			_sportManager = sportManager;
		}

		[HttpPost("InsertSport")]
		public async Task<IActionResult> InsertSport(Sport newSport)
		{
			var isCorrect = await _sportManager.InsertSport(newSport);
			return isCorrect ? Ok() : StatusCode(500, "Failed to insert Sport");
		}

		[HttpPut("UpdateSport")]
		public async Task<IActionResult> UpdateSport(Sport oldSport)
		{
			var isCorrect = await _sportManager.UpdateSport(oldSport);
			return isCorrect ? Ok() : StatusCode(500, "Failed to update Sport");
		}

		[HttpPut("DeleteSport")]
		public async Task<IActionResult> DeleteSport(int accountId, Sport oldSport)
		{
			var isCorrect = await _sportManager.DeleteSport(accountId, oldSport);
			return isCorrect ? Ok() : StatusCode(500, "Failed to delete Sport");
		}	

		[HttpPost("InsertAccountDoesSport")]
		public async Task<IActionResult> InsertAccountDoesSport(Account_does_Sport newSport)
		{
			var isCorrect = await _sportManager.InsertAccountDoesSport(newSport);
			return isCorrect ? Ok() : StatusCode(500, "Failed to insert AccountDoesSport");
		}

		[HttpPut("DeleteAccountDoesSport")]
		public async Task<IActionResult> DeleteAccountDoesSport(Account_does_Sport oldSport)
		{
			var isCorrect = await _sportManager.DeleteAccountDoesSport(oldSport);
			return isCorrect ? Ok() : StatusCode(500, "Failed to delete AccountDoesSport");
		}

		[HttpPost("InsertRoutine")]
		public async Task<IActionResult> InsertRoutine(Routine newRoutine)
		{
			var isCorrect = await _sportManager.InsertRoutine(newRoutine);
			return isCorrect ? Ok() : StatusCode(500, "Failed to insert Routine");
		}

		[HttpPut("UpdateRoutine")]
		public async Task<IActionResult> UpdateRoutine(Routine oldRoutine)
		{
			var isCorrect = await _sportManager.UpdateRoutine(oldRoutine);
			return isCorrect ? Ok() : StatusCode(500, "Failed to update Routine");
		}

		[HttpPut("DeleteRoutine")]
		public async Task<IActionResult> DeleteRoutine(Routine oldRoutine)
		{
			var isCorrect = await _sportManager.DeleteRoutine(oldRoutine);
			return isCorrect ? Ok() : StatusCode(500, "Failed to delete Routine");
		}

		[HttpPost("InsertExercise")]
		public async Task<IActionResult> InsertExercise(Exercise newExercise)
		{
			var isCorrect = await _sportManager.InsertExercise(newExercise);
			return isCorrect ? Ok() : StatusCode(500, "Failed to insert Exercise");
		}

		[HttpPut("UpdateExercise")]
		public async Task<IActionResult> UpdateExercise(Exercise oldExercise)
		{
			var isCorrect = await _sportManager.UpdateExercise(oldExercise);
			return isCorrect ? Ok() : StatusCode(500, "Failed to update Exercise");
		}

		[HttpPut("DeleteExercise")]
		public async Task<IActionResult> DeleteExercise(Exercise oldExercise)
		{
			var isCorrect = await _sportManager.DeleteExercise(oldExercise);
			return isCorrect ? Ok() : StatusCode(500, "Failed to delete Exercise");
		}

		[HttpPost("InsertSet")]
		public async Task<IActionResult> InsertSet(Sets newSet)
		{
			var isCorrect = await _sportManager.InsertSet(newSet);
			return isCorrect ? Ok() : StatusCode(500, "Failed to insert Set");
		}

		[HttpPut("UpdateSet")]
		public async Task<IActionResult> UpdateSet(Sets oldSet)
		{
			var isCorrect = await _sportManager.UpdateSet(oldSet);
			return isCorrect ? Ok() : StatusCode(500, "Failed to update Set");
		}

		[HttpDelete("DeleteSet/{id}")]
		public async Task<IActionResult> DeleteSet(int id)
		{
			var isCorrect = await _sportManager.DeleteSet(id);
			return isCorrect ? Ok() : StatusCode(500, "Failed to delete Set");
		}

		[HttpPost("InsertWorkout")]
		public async Task<IActionResult> InsertWorkout(Workout newWorkout)
		{
			var isCorrect = await _sportManager.InsertWorkout(newWorkout);
			return isCorrect ? Ok() : StatusCode(500, "Failed to insert Workout");
		}

		[HttpPut("UpdateWorkout")]
		public async Task<IActionResult> UpdateWorkout(Workout oldWorkout)
		{
			var isCorrect = await _sportManager.UpdateWorkout(oldWorkout);
			return isCorrect ? Ok() : StatusCode(500, "Failed to update Workout");
		}

		[HttpPut("DeleteWorkout")]
		public async Task<IActionResult> DeleteWorkout(Workout oldWorkout)
		{
			var isCorrect = await _sportManager.DeleteWorkout(oldWorkout);
			return isCorrect ? Ok() : StatusCode(500, "Failed to delete Workout");
		}

		[HttpGet("GetAllSports")]
		public async Task<IActionResult> GetAllSports(int accountId)
		{
			var sports = await _sportManager.GetAllSports(accountId);
			return Ok(sports);
		}

		[HttpGet("GetAccountsSports")]
		public async Task<IActionResult> GetAccountsSports(int accountId)
		{
			var sports = await _sportManager.GetAccountsSports(accountId);
			return Ok(sports);
		}

		[HttpGet("GetAccountDoesSport")]
		public async Task<IActionResult> GetAccountDoesSport(int accountId, int sportId)
		{
			var accountDoesSport = await _sportManager.GetAccountDoesSport(accountId, sportId);
			return Ok(accountDoesSport);
		}

		[HttpGet("GetAccountDoesSportByID")]
		public async Task<IActionResult> GetAccountDoesSportByID(int accountDoesSportId)
		{
			var accountDoesSport = await _sportManager.GetAccountDoesSport(accountDoesSportId);
			return Ok(accountDoesSport);
		}

		[HttpGet("GetAccountDoesSports")]
		public async Task<IActionResult> GetAccountDoesSports(int accountId)
		{
			var accountDoesSports = await _sportManager.GetAccountDoesSports(accountId);
			return Ok(accountDoesSports);
		}

		[HttpGet("GetRoutine")]
		public async Task<IActionResult> GetRoutine(int routineId)
		{
			var routine = await _sportManager.GetRoutine(routineId);
			return Ok(routine);
		}

		[HttpGet("GetRoutines")]
		public async Task<IActionResult> GetRoutines(int accountDoesSportId)
		{
			var routines = await _sportManager.GetRoutines(accountDoesSportId);
			return Ok(routines);
		}

		[HttpGet("GetRoutineExample")]
		public async Task<IActionResult> GetRoutineExample(int accountDoesId, int RoutineId)
		{
			var example = await _sportManager.GetRoutineExample(accountDoesId, RoutineId);
			return Ok(example);
		}

		[HttpGet("GetExercise")]
		public async Task<IActionResult> GetExercise(int exerciseId)
		{
			var exercise = await _sportManager.GetExercise(exerciseId);
			return Ok(exercise);
		}

		[HttpGet("GetExercises")]
		public async Task<IActionResult> GetExercises(int accountId, int sportId)
		{
			var exercises = await _sportManager.GetExercises(accountId, sportId);
			return Ok(exercises);
		}

		[HttpGet("GetWorkout")]
		public async Task<IActionResult> GetWorkout(int workoutId)
		{
			var workout = await _sportManager.GetWorkout(workoutId);
			return Ok(workout);
		}

		[HttpGet("GetWorkouts")]
		public async Task<IActionResult> GetWorkouts(int accountDoesSportId, bool onlyExamples)
		{
			var workouts = await _sportManager.GetWorkouts(accountDoesSportId, onlyExamples);
			return Ok(workouts);
		}

		[HttpGet("GetSetsByExercise")]
		public async Task<IActionResult> GetSetsByExercise(int exerciseId)
		{
			var sets = await _sportManager.GetSetsByExercise(exerciseId);
			return Ok(sets);
		}

		[HttpGet("GetSetsByWorkout")]
		public async Task<IActionResult> GetSetsByWorkout(int workoutId)
		{
			var sets = await _sportManager.GetSetsByWorkout(workoutId);
			return Ok(sets);
		}

		[HttpGet("GetSetsByBoth")]
		public async Task<IActionResult> GetSetsByBoth(int exerciseId, int workoutId)
		{
			var sets = await _sportManager.GetSetsByBoth(exerciseId, workoutId);
			return Ok(sets);
		}
	}
}
