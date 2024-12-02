using System.Net.Http.Json;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models.Sports;

namespace MauiBlazorWeb.Shared.Services
{
	public class SportAPIService : ISportAPIService
	{
		public SportAPIService(HttpClient httpClientIn)
		{
			httpClient = httpClientIn;
		}

		private HttpClient httpClient;

		public async Task<bool> InsertSport(Sport newSport)
		{
			var response = await httpClient.PostAsJsonAsync("api/Sport/InsertSport", newSport);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateSport(Sport oldSport)
		{
			var response = await httpClient.PutAsJsonAsync("api/Sport/UpdateSport", oldSport);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> DeleteSport(int accountId, Sport oldSport)
		{
			var response = await httpClient.PutAsJsonAsync($"api/Sport/DeleteSport?accountId={accountId}", oldSport);
			return response.IsSuccessStatusCode;
		}


		public async Task<bool> InsertAccountDoesSport(Account_does_Sport newSport)
		{
			var response = await httpClient.PostAsJsonAsync("api/Sport/InsertAccountDoesSport", newSport);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> DeleteAccountDoesSport(Account_does_Sport oldSport)
		{
			var response = await httpClient.PutAsJsonAsync($"api/Sport/DeleteAccountDoesSport", oldSport);
			return response.IsSuccessStatusCode;
		}


		public async Task<bool> InsertRoutine(Routine newRoutine)
		{
			var response = await httpClient.PostAsJsonAsync("api/Sport/InsertRoutine", newRoutine);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateRoutine(Routine oldRoutine)
		{
			var response = await httpClient.PutAsJsonAsync($"api/Sport/UpdateRoutine", oldRoutine);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> DeleteRoutine(Routine oldRoutine)
		{
			var response = await httpClient.PutAsJsonAsync($"api/Sport/DeleteRoutine", oldRoutine);
			return response.IsSuccessStatusCode;
		}


		public async Task<bool> InsertExercise(Exercise newExercise)
		{
			var response = await httpClient.PostAsJsonAsync("api/Sport/InsertExercise", newExercise);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateExercise(Exercise oldExercise)
		{
			var response = await httpClient.PutAsJsonAsync($"api/Sport/UpdateExercise", oldExercise);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> DeleteExercise(Exercise oldExercise)
		{
			var response = await httpClient.PutAsJsonAsync($"api/Sport/DeleteExercise", oldExercise);
			return response.IsSuccessStatusCode;
		}


		public async Task<bool> InsertSet(Sets newSet)
		{
			var response = await httpClient.PostAsJsonAsync("api/Sport/InsertSet", newSet);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateSet(Sets oldSet)
		{
			var response = await httpClient.PutAsJsonAsync($"api/Sport/UpdateSet", oldSet);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> DeleteSet(Sets oldSet)
		{
			var response = await httpClient.DeleteAsync($"api/Sport/DeleteSet/{oldSet.Id}");
			return response.IsSuccessStatusCode;
		}


		public async Task<bool> InsertWorkout(Workout newWorkout)
		{
			var response = await httpClient.PostAsJsonAsync("api/Sport/InsertWorkout", newWorkout);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> UpdateWorkout(Workout oldWorkout)
		{
			var response = await httpClient.PutAsJsonAsync($"api/Sport/UpdateWorkout", oldWorkout);
			return response.IsSuccessStatusCode;
		}
		public async Task<bool> DeleteWorkout(Workout oldWorkout)
		{
			var response = await httpClient.PutAsJsonAsync($"api/Sport/DeleteWorkout", oldWorkout);
			return response.IsSuccessStatusCode;
		}


		public async Task<List<Sport>> GetAllSports(int accountId)
		{
			try
			{
				var response = await httpClient.GetAsync($"api/Sport/GetAllSports?accountId={accountId}");
				response.EnsureSuccessStatusCode();
				return await response.Content.ReadFromJsonAsync<List<Sport>>() ?? new List<Sport>();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error calling API: {ex.Message}");
				throw;
			}


			//var response = await httpClient.GetAsync($"api/Sport/GetAllSports?accountId={accountId}");
			//return await response.Content.ReadFromJsonAsync<List<Sport>>() ?? [];
		}
		public async Task<List<Sport>> GetAccountsSports(int accountId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetAccountsSports?accountId={accountId}");
			return await response.Content.ReadFromJsonAsync<List<Sport>>() ?? [];
		}

		public async Task<List<Account_does_Sport>> GetAccountDoesSport(int accountId, int sportId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetAccountDoesSport?accountId={accountId}&sportId={sportId}");
			return await response.Content.ReadFromJsonAsync<List<Account_does_Sport>>() ?? [];
		}
		public async Task<List<Account_does_Sport>> GetAccountDoesSport(int accountDoesSportId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetAccountDoesSportByID?accountDoesSportId={accountDoesSportId}");
			return await response.Content.ReadFromJsonAsync<List<Account_does_Sport>>() ?? [];
		}
		public async Task<List<Account_does_Sport>> GetAccountDoesSports(int accountId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetAccountDoesSports?accountId={accountId}");
			return await response.Content.ReadFromJsonAsync<List<Account_does_Sport>>() ?? [];
		}

		public async Task<List<Routine>> GetRoutine(int routineId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetRoutine?routineId={routineId}");
			return await response.Content.ReadFromJsonAsync<List<Routine>>() ?? [];
		}
		public async Task<List<Routine>> GetRoutines(int accountDoesSportId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetRoutines?accountDoesSportId={accountDoesSportId}");
			return await response.Content.ReadFromJsonAsync<List<Routine>>() ?? [];
		}

		public async Task<List<Workout>> GetRoutineExample(int accountDoesId, int RoutineId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetRoutineExample?accountDoesId={accountDoesId}&RoutineId={RoutineId}");
			return await response.Content.ReadFromJsonAsync<List<Workout>>() ?? [];
		}

		public async Task<List<Exercise>> GetExercise(int exerciseId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetExercise?exerciseId={exerciseId}");
			return await response.Content.ReadFromJsonAsync<List<Exercise>>() ?? [];
		}
		public async Task<List<Exercise>> GetExercises(int accountId, int sportId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetExercises?accountId={accountId}&sportId={sportId}");
			return await response.Content.ReadFromJsonAsync<List<Exercise>>() ?? [];
		}

		public async Task<List<Workout>> GetWorkout(int workoutId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetWorkout?workoutId={workoutId}");
			return await response.Content.ReadFromJsonAsync<List<Workout>>() ?? [];
		}
		public async Task<List<Workout>> GetWorkouts(int accountDoesSportId, bool onlyExamples)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetWorkouts?accountDoesSportId={accountDoesSportId}&onlyExamples={onlyExamples}");
			return await response.Content.ReadFromJsonAsync<List<Workout>>() ?? [];
		}

		public async Task<List<Sets>> GetSetsByExercise(int exerciseId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetSetsByExercise?exerciseId={exerciseId}");
			return await response.Content.ReadFromJsonAsync<List<Sets>>() ?? [];
		}

		public async Task<List<Sets>> GetSetsByWorkout(int workoutId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetSetsByWorkout?workoutId={workoutId}");
			return await response.Content.ReadFromJsonAsync<List<Sets>>() ?? [];
		}

		public async Task<List<Sets>> GetSetsByBoth(int exerciseId, int workoutId)
		{
			var response = await httpClient.GetAsync($"api/Sport/GetSetsByBoth?exerciseId={exerciseId}&workoutId={workoutId}");
			return await response.Content.ReadFromJsonAsync<List<Sets>>() ?? [];
		}
	}
}
