using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models.Diaries;
using MauiBlazorWeb.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace MauiBlazorWeb.Shared.Components.Widgets.Bases
{
	public enum TimePeriod { Week, Month, Year, All }
	public class DiaryCompBase : ObserverComp
	{
		[Inject] protected IAppState AppState { get; set; } = default!;
		[Inject] protected IDiaryManager DiaryManager { get; set; } = default!;
		[Parameter] public bool IsHabit { get; set; }

		protected List<Diary_log_column> allCols = [];
		protected List<Diary_log_post> allPosts = [];

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

		public override async Task UpdateObserver()
		{
			await UpdateTables();
			await base.UpdateObserver();
		}

		protected virtual async Task UpdateTables()
		{
			allCols = await DiaryManager.GetDiaryCols(AppState.CurrentUser.Id, IsHabit);
			allPosts = await DiaryManager.GetDiaryPosts(AppState.CurrentUser.Id, IsHabit);
		}

		private async Task ToggleHabitValue(int colId, DateTime day)
		{
			await DiaryManager.ToggleHabitValue(colId, day);
			await Subject.UpdateObservers();
		}

		protected async Task OnToggleHabitValue(int colId, DateTime day)
		{
			await AppState.ShowLoadingScreenWhileAwaiting(() => ToggleHabitValue(colId, day));
		}

		protected async Task RefreshDiaryComps()
		{
			await AppState.ShowLoadingScreenWhileAwaiting(Subject.UpdateObservers);
		}

		protected static bool FilterPostsByTimePeriod(DateTime postDate, DateTime refDate, TimePeriod timeSpan)
		{
			return timeSpan switch
			{
				TimePeriod.Week => postDate > refDate.AddDays(-7),
				TimePeriod.Month => postDate.Month == refDate.Month && postDate.Year == refDate.Year,
				TimePeriod.Year => postDate.Year == refDate.Year,
				TimePeriod.All => true,
				_ => false,
			};
		}
	}
}
