using MauiBlazorWeb.Shared.Models.Sports;

namespace MauiBlazorWeb.Shared.Interfaces
{
	public interface ISportManager
	{
		Task<bool> DeleteAccountDoesSport(Account_does_Sport oldSport);
		Task<bool> DeleteExercise(Exercise oldExercise);
		Task<bool> DeleteRoutine(Routine oldRoutine);
		Task<bool> DeleteSet(Sets oldSet);
		Task<bool> DeleteSport(Sport oldSport);
		Task<bool> DeleteWorkout(Workout oldWorkout);

		Task<bool> InsertAccountDoesSport(Account_does_Sport newSport);
		Task<bool> InsertExercise(Exercise newExercise);
		Task<bool> InsertRoutine(Routine newRoutine);
		Task<bool> InsertSet(Sets newSet);
		Task<bool> InsertSport(Sport newSport);
		Task<bool> InsertWorkout(Workout newWorkout);

		Task<bool> UpdateExercise(Exercise oldExercise);
		Task<bool> UpdateRoutine(Routine oldRoutine);
		Task<bool> UpdateSet(Sets oldSet);
		Task<bool> UpdateSport(Sport oldSport);
		Task<bool> UpdateWorkout(Workout oldWorkout);

		Task<List<Account_does_Sport>> GetAccountDoesSport(int accountId, int sportId);
		Task<List<Account_does_Sport>> GetAccountDoesSport(int accountDoesSportId);
		Task<List<Account_does_Sport>> GetAccountDoesSports(int accountId);
		Task<List<Sport>> GetAccountsSports(int accountId);
		Task<List<Sport>> GetAllSports(int accountId);
		Task<List<Exercise>> GetExercises(int accountId, int sportId);
		Task<List<Routine>> GetRoutine(int routineId);
		Task<List<Routine>> GetRoutines(Account_does_Sport accountDoesSport);
		Task<List<Sets>> GetSetsByExercise(int exerciseId);
		Task<List<Sets>> GetSetsByWorkout(int workoutId);
		Task<List<Workout>> GetWorkouts(Account_does_Sport accountDoesSport);
	}
}