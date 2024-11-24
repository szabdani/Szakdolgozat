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
		[Inject] protected IAppState _appState { get; set; }
		[Inject] protected ISportManager _sportManager { get; set; }
		[Inject] protected NavigationManager navigation { get; set; }

		protected List<MauiBlazorWeb.Shared.Models.Sports.Sport> allSports;
		protected List<Account_does_Sport> allAccountDoesSports;
		protected bool hasInvalidParameter = false;

		protected DateTime firstDate;
		protected List<DateTime> allDatesSinceReg;
		
		public SportCompBase()
		{
			_appState = new AppState();
			_sportManager = new SportManager();

			allSports = new List<MauiBlazorWeb.Shared.Models.Sports.Sport>();
			allAccountDoesSports = new List<Account_does_Sport>();

			allDatesSinceReg = new List<DateTime>();
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			firstDate = _appState.CurrentUser.RegistrationDate;

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
					if (first.Account_Id != _appState.CurrentUser.Id)
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
				var list = await _sportManager.GetRoutine(RoutineId);
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
						if (firstAccDoes == null || firstAccDoes.Account_Id != _appState.CurrentUser.Id)
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
				var list = await _sportManager.GetWorkout(WorkoutId);
				var first = list.FirstOrDefault();
				if (first == null)
					hasInvalidParameter = true;
				else
				{
					var firstAccDoes = allAccountDoesSports.FirstOrDefault(a => a.Id == first.Account_does_Sport_Id);
					if (firstAccDoes == null || firstAccDoes.Account_Id != _appState.CurrentUser.Id)
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
				var list = await _sportManager.GetExercise(ExerciseId);
				var first = list.FirstOrDefault();
				if (first == null)
					hasInvalidParameter = true;
				else
				{
					if(first.Status == SportStatus.Deleted)
						hasInvalidParameter= true;
					else if(first.Status == SportStatus.Private && first.Creator_Account_Id != _appState.CurrentUser.Id)
						hasInvalidParameter = true;
					else
						retVal = first;
				}
			}
			else
				hasInvalidParameter = true;

			return retVal;
		}

		public async Task OnStartWorkout(int accountDoesId, int routineId = 0, bool isRoutineExample = false)
		{
			await _appState.ShowLoadingScreenWhileAwaiting(() => StartWorkout(accountDoesId, routineId, isRoutineExample));
		}

		public async Task StartWorkout(int accountDoesId, int routineId, bool isRoutineExample)
		{
			Workout newWorkout = new Workout
			{
				Starttime = DateTime.Now,
				IsDone = false,
				IsRoutineExample = isRoutineExample,
				Account_does_Sport_Id = accountDoesId
			};

			if (routineId != 0)
				newWorkout.Routine_Id = routineId;


			bool isCorrect = await _sportManager.InsertWorkout(newWorkout);
			if (!isCorrect)
				throw new Exception($"Sorry, we could not save your Workout");

			var list = await _sportManager.GetWorkouts(accountDoesId);
			int insertedId = list.Last().Id;

			navigation.NavigateTo($"new-workout/id={insertedId}", true);
		}

		public override async Task UpdateObserver()
		{
			await UpdateTables();
			await base.UpdateObserver();
		}

		protected virtual async Task UpdateTables()
		{
			allSports = await _sportManager.GetAllSports(_appState.CurrentUser.Id);
			allAccountDoesSports = await _sportManager.GetAccountDoesSports(_appState.CurrentUser.Id);
		}

		protected async Task RefreshSportComps()
		{
			await _appState.ShowLoadingScreenWhileAwaiting(_subject.UpdateObservers);
		}
	}
}
