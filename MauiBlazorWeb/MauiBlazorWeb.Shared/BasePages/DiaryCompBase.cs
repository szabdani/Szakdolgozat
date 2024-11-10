using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Components.DiaryComps;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models.Diaries;
using MauiBlazorWeb.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace MauiBlazorWeb.Shared.BasePages
{
	public class DiaryCompBase : ComponentBase
	{
		[Inject] IAppState _appState { get; set; }
		[Inject] IDiaryManager _diaryManager { get; set; }

		[Parameter] public Func<Task>? RerenderParent { get; set; }
		[Parameter] public bool IsHabit { get; set; }

		protected List<Diary_log_column> allCols;
		protected List<Diary_log_post> allPosts;

		public DiaryCompBase()
		{
			_appState = new AppState();
			_diaryManager = new DiaryManager();

			allCols = new List<Diary_log_column>();
			allPosts = new List<Diary_log_post>();
		}

		protected override async Task OnInitializedAsync()
		{
			await RerenderDiaryComp();
		}

		protected async Task LoadTask(Func<Task>? action, bool callParent = true)
		{
			await InvokeAsync(() => _appState.MainLayout.SetLoadingScreen(true));

			try
			{
				if (action is not null)
					await action();
			}
			finally
			{
				await UpdateTables();
				if (callParent && RerenderParent is not null)
					await InvokeAsync(RerenderParent);
				await InvokeAsync(() => _appState.MainLayout.SetLoadingScreen(false));
			}
		}

		public async Task RerenderDiaryComp()
		{
			await LoadTask(null, false);
		}

		protected virtual async Task UpdateTables()
		{
			allCols = await _diaryManager.GetDiaryCols(_appState.CurrentUser.Id, IsHabit);
			allPosts = await _diaryManager.GetDiaryPosts(_appState.CurrentUser.Id, IsHabit);
		}

		protected async Task DeleteRow(DateTime date)
		{
			await LoadTask(() => _diaryManager.DeleteSameDatePosts(_appState.CurrentUser.Id, date, IsHabit));
		}


		protected async Task ToggleHabitValue(int colId, DateTime day)
		{
			await LoadTask(() => _diaryManager.ToggleHabitValue(colId, day));
		}
	}
}
