﻿using System;
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
		[Inject] protected ISportAPIService SportAPI { get; set; } = default!;
		[Inject] protected NavigationManager Navigation { get; set; } = default!;

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
			hasInvalidParameter = false;
			await ValidateParameters();
		}
		protected virtual async Task ValidateParameters()
		{
			await UpdateTables();
		}

		protected async Task<MauiBlazorWeb.Shared.Models.Sports.Sport> ValidateSport(int SportId)
		{
			var retVal = new MauiBlazorWeb.Shared.Models.Sports.Sport();

			if (SportId != 0)
			{
				var allSports = await SportAPI.GetAccountsSports(AppState.CurrentUser.Id);
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

		protected async Task<Account_does_Sport> ValidateAccountDoesSport(int AccountDoesId)
		{
			var retVal = new Account_does_Sport();

			if (AccountDoesId != 0)
			{
				var allAccountDoesSports = await SportAPI.GetAccountDoesSports(AppState.CurrentUser.Id);
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

		protected async Task<Workout> ValidateWorkout(int WorkoutId)
		{
			var retVal = new Workout();

			if (WorkoutId != 0)
			{
				var list = await SportAPI.GetWorkout(WorkoutId);
				var first = list.FirstOrDefault();
				if (first == null)
					hasInvalidParameter = true;
				else
				{
					var allAccountDoesSports = await SportAPI.GetAccountDoesSports(AppState.CurrentUser.Id);
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

			bool isCorrect = await SportAPI.InsertWorkout(newWorkout);
			if (!isCorrect)
				throw new Exception($"Sorry, we could not save your Workout");

			var list = await SportAPI.GetWorkouts(accountDoesId, false);
			int insertedId = list.Last().Id;

			Navigation.NavigateTo($"workout/id={insertedId}", true);
		}
		private async Task DeletRoutineById(int accountDoesSportId, int routineId)
		{
			var list = await SportAPI.GetRoutines(accountDoesSportId);
			var first = list.First(r => r.Id == routineId);

			await DeleteRoutine(first);
		}

		private async Task DeleteRoutine(Routine routine)
		{
			bool isCorrect = await SportAPI.DeleteRoutine(routine);
			if (!isCorrect)
				throw new Exception($"Sorry, could not delete your Routine");

			await RefreshSportComps();
		}
		private async Task DiscardWorkout(Workout workout)
		{
			bool isCorrect = await SportAPI.DeleteWorkout(workout);
			if (!isCorrect)
				throw new Exception($"Sorry, we could not discard your Workout");

			Navigation.NavigateTo($"/sports/id={workout.Account_does_Sport_Id}", true);
		}

		protected async Task RefreshSportComps()
		{
			await AppState.ShowLoadingScreenWhileAwaiting(Subject.UpdateObservers);
		}
	}
}
