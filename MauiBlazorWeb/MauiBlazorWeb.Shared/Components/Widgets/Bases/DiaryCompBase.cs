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
		[Inject] protected IAppState _appState { get; set; }
		[Inject] protected IDiaryManager _diaryManager { get; set; }
		[Parameter] public bool IsHabit { get; set; }

		protected List<Diary_log_column> allCols = new();
		protected List<Diary_log_post> allPosts = new();

		protected DateTime firstDate = new();
		protected List<DateTime> allDatesSinceReg = new();

		protected override void OnInitialized()
		{
			base.OnInitialized();

			firstDate = _appState.CurrentUser.RegistrationDate;

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
			allCols = await _diaryManager.GetDiaryCols(_appState.CurrentUser.Id, IsHabit);
			allPosts = await _diaryManager.GetDiaryPosts(_appState.CurrentUser.Id, IsHabit);
		}

		private async Task ToggleHabitValue(int colId, DateTime day)
		{
			await _diaryManager.ToggleHabitValue(colId, day);
			await _subject.UpdateObservers();
		}

		protected async Task OnToggleHabitValue(int colId, DateTime day)
		{
			await _appState.ShowLoadingScreenWhileAwaiting(() => ToggleHabitValue(colId, day));
		}

		protected async Task RefreshDiaryComps()
		{
			await _appState.ShowLoadingScreenWhileAwaiting(_subject.UpdateObservers);
		}

		protected bool FilterPostsByTimePeriod(DateTime postDate, DateTime refDate, TimePeriod timeSpan)
		{
			switch (timeSpan)
			{
				case TimePeriod.Week:
					return postDate > refDate.AddDays(-7);
				case TimePeriod.Month:
					return postDate.Month == refDate.Month && postDate.Year == refDate.Year;
				case TimePeriod.Year:
					return postDate.Year == refDate.Year;
				case TimePeriod.All:
					return true;
				default:
					return false;
			}
		}
	}
}
