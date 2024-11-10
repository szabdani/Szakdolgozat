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
		[Inject] IAppState appState { get; set; }
		[Inject] IDiaryManager diaryManager { get; set; }

		[Parameter] public Func<Task>? RerenderParent { get; set; }
		[Parameter] public bool IsHabit { get; set; }

		protected List<Diary_log_column> allCols;
		protected List<Diary_log_post> allPosts;

		public DiaryCompBase()
		{
			appState = new AppState();
			diaryManager = new DiaryManager();

			allCols = new List<Diary_log_column>();
			allPosts = new List<Diary_log_post>();
		}

		protected override async Task OnInitializedAsync()
		{
            await LoadTask(null, false);
        }

		protected async Task LoadTask(Func<Task>? action, bool callParent = true)
		{
			await InvokeAsync(() => appState.MainLayout.SetLoadingScreen(true));

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
				await InvokeAsync(() => appState.MainLayout.SetLoadingScreen(false));
			}
		}

		public async Task RerenderDiaryComp()
		{
			await LoadTask(null);
		}

        public async Task UpdateDiaryComp()
        {
            await LoadTask(null, false);
        }


        protected virtual async Task UpdateTables()
		{
			allCols = await diaryManager.GetDiaryCols(appState.CurrentUser.Id, IsHabit);
			allPosts = await diaryManager.GetDiaryPosts(appState.CurrentUser.Id, IsHabit);
		}

		protected async Task DeleteRow(DateTime date)
		{
			await LoadTask(() => diaryManager.DeleteSameDatePosts(appState.CurrentUser.Id, date, IsHabit));
		}


		protected async Task ToggleHabitValue(int colId, DateTime day)
		{
			await LoadTask(() => diaryManager.ToggleHabitValue(colId, day));
		}
	}
}
