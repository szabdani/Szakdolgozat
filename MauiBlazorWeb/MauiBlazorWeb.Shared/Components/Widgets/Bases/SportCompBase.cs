using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models.Sports;
using MauiBlazorWeb.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace MauiBlazorWeb.Shared.Components.Widgets.Bases
{
	public class SportCompBase : ObserverComp
	{
		[Inject] protected IAppState AppState { get; set; } = default!;
		[Inject] protected ISportManager SportManager { get; set; } = default!;
		[Inject] protected NavigationManager Navigation { get; set; } = default!;

		protected List<MauiBlazorWeb.Shared.Models.Sports.Sport> allSports = [];
		protected List<Account_does_Sport> allAccountDoesSports = [];
		protected bool hasInvalidParameter = false;

		protected DateTime firstDate = new();
		protected List<DateTime> allDatesSinceReg = [];

		protected override void OnInitialized()
		{
			base.OnInitialized();

			firstDate = AppState.CurrentUser.RegistrationDate;

			for (DateTime date = firstDate; date <= DateTime.Today; date = date.AddDays(1))
			{
				allDatesSinceReg.Add(date);
			}
		}

		protected override async Task OnParametersSetAsync()
		{
			await ValidateParameters();
		}
		protected virtual async Task ValidateParameters()
		{
			await UpdateTables();
		}

		protected MauiBlazorWeb.Shared.Models.Sports.Sport ValidateSport(int SportId)
		{
			var retVal = new MauiBlazorWeb.Shared.Models.Sports.Sport();

			if (SportId != 0)
			{
				var first = allSports.FirstOrDefault(s => s.Id == SportId);
				if (first == null)
					hasInvalidParameter = true;
				else
					retVal = first;
			}
			else
				hasInvalidParameter = true;

			return retVal;
		}

		protected Account_does_Sport ValidateAccountDoesSport(int AccountDoesId)
		{
			var retVal = new Account_does_Sport();

			if (AccountDoesId != 0)
			{
				var first = allAccountDoesSports.FirstOrDefault(a => a.Id == AccountDoesId);
				if (first == null)
					hasInvalidParameter = true;
				else
				{
					if (first.Account_Id != AppState.CurrentUser.Id)
						hasInvalidParameter = true;
					else
						retVal = first;
				}
			}
			else
				hasInvalidParameter = true;

			return retVal;
		}

		protected async Task<Routine> ValidateRoutine(int RoutineId)
		{
			var retVal = new Routine();

			if (RoutineId != 0)
			{
				var list = await SportManager.GetRoutine(RoutineId);
				var first = list.FirstOrDefault();
				if (first == null)
					hasInvalidParameter = true;
				else
				{
					if (first.Status == SportStatus.Deleted)
						hasInvalidParameter = true;
					else 
					{
						var firstAccDoes = allAccountDoesSports.FirstOrDefault(a => a.Id == first.Account_does_Sport_Id);
						if (firstAccDoes == null || firstAccDoes.Account_Id != AppState.CurrentUser.Id)
							hasInvalidParameter = true;
						else
							retVal = first;
					}
				}
			}
			else
				hasInvalidParameter = true;

			return retVal;
		}

		protected async Task<Workout> ValidateWorkout(int WorkoutId)
		{
			var retVal = new Workout();

			if (WorkoutId != 0)
			{
				var list = await SportManager.GetWorkout(WorkoutId);
				var first = list.FirstOrDefault();
				if (first == null)
					hasInvalidParameter = true;
				else
				{
					var firstAccDoes = allAccountDoesSports.FirstOrDefault(a => a.Id == first.Account_does_Sport_Id);
					if (firstAccDoes == null || firstAccDoes.Account_Id != AppState.CurrentUser.Id)
						hasInvalidParameter = true;
					else
						retVal = first;
				}
			}
			else
				hasInvalidParameter = true;

			return retVal;
		}

		protected async Task<Exercise> ValidateExercise(int ExerciseId)
		{
			var retVal = new Exercise();

			if (ExerciseId != 0)
			{
				var list = await SportManager.GetExercise(ExerciseId);
				var first = list.FirstOrDefault();
				if (first == null)
					hasInvalidParameter = true;
				else
				{
					if(first.Status == SportStatus.Deleted)
						hasInvalidParameter= true;
					else if(first.Status == SportStatus.Private && first.Creator_Account_Id != AppState.CurrentUser.Id)
						hasInvalidParameter = true;
					else
						retVal = first;
				}
			}
			else
				hasInvalidParameter = true;

			return retVal;
		}

		protected async Task<Workout> GetRoutineExample(int accountDoesId, int RoutineId)
		{
			var list = await SportManager.GetWorkouts(accountDoesId, true);
			return list.First(w => w.Routine_Id == RoutineId);
		}

		public async Task OnStartWorkout(int accountDoesId, int routineId = 0)
		{
			await AppState.ShowLoadingScreenWhileAwaiting(() => StartWorkout(accountDoesId, routineId));
		}

		public async Task OnDiscardWorkout(Workout workout)
		{
			await AppState.ShowLoadingScreenWhileAwaiting(() => DiscardWorkout(workout));
		}
		public async Task OnDeleteRoutine(Routine routine)
		{
			await AppState.ShowLoadingScreenWhileAwaiting(() => DeleteRoutine(routine));
		}

		public async Task OnDeletRoutineById(int accountDoesSportId, int routineId)
		{
			await AppState.ShowLoadingScreenWhileAwaiting(() => DeletRoutineById(accountDoesSportId, routineId));
		}

		public async Task StartWorkout(int accountDoesId, int routineId)
		{
			Workout newWorkout = new()
			{
				Name = "New Workout",
				Starttime = DateTime.Now,
				IsDone = false,
				IsRoutineExample = false,
				Account_does_Sport_Id = accountDoesId
			};

			if (routineId != 0)
				newWorkout.Routine_Id = routineId;

			bool isCorrect = await SportManager.InsertWorkout(newWorkout);
			if (!isCorrect)
				throw new Exception($"Sorry, we could not save your Workout");

			var list = await SportManager.GetWorkouts(accountDoesId, false);
			int insertedId = list.Last().Id;

			Navigation.NavigateTo($"workout/id={insertedId}", true);
		}
		private async Task DeletRoutineById(int accountDoesSportId, int routineId)
		{
			var list = await SportManager.GetRoutines(accountDoesSportId);
			var first = list.First(r => r.Id == routineId);

			await DeleteRoutine(first);
		}

		private async Task DeleteRoutine(Routine routine)
		{
			bool isCorrect = await SportManager.DeleteRoutine(routine);
			if (!isCorrect)
				throw new Exception($"Sorry, could not delete your Routine");

			Navigation.NavigateTo($"/sports/id={routine.Account_does_Sport_Id}", true);
		}
		private async Task DiscardWorkout(Workout workout)
		{
			bool isCorrect = await SportManager.DeleteWorkout(workout);
			if (!isCorrect)
				throw new Exception($"Sorry, we could not discard your Workout");

			Navigation.NavigateTo($"/sports/id={workout.Account_does_Sport_Id}", true);
		}

		public override async Task UpdateObserver()
		{
			await UpdateTables();
			await base.UpdateObserver();
		}

		protected virtual async Task UpdateTables()
		{
			allSports = await SportManager.GetAllSports(AppState.CurrentUser.Id);
			allAccountDoesSports = await SportManager.GetAccountDoesSports(AppState.CurrentUser.Id);
		}

		protected async Task RefreshSportComps()
		{
			await AppState.ShowLoadingScreenWhileAwaiting(Subject.UpdateObservers);
		}
	}
}
