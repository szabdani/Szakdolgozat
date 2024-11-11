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

namespace MauiBlazorWeb.Shared.Bases
{
	public class DiaryCompBase : ComponentBase
	{
		[Inject] protected IAppState _appState { get; set; }
		[Inject] protected IDiaryManager _diaryManager { get; set; }

		[Inject] protected IDiaryCompSubject _diarySubject { get; set; }

		[Parameter] public bool IsHabit { get; set; }

		protected List<Diary_log_column> allCols;
		protected List<Diary_log_post> allPosts;

		protected DateTime firstDate;
		protected List<DateTime> allDatesSinceReg;

		public DiaryCompBase()
		{
			_appState = new AppState();
			_diaryManager = new DiaryManager();
			_diarySubject = new DiaryCompSubject();

			allCols = new List<Diary_log_column>();
			allPosts = new List<Diary_log_post>();

			allDatesSinceReg = new List<DateTime>();
		}

		protected override void OnInitialized()
		{
			_diarySubject.Attach(this);

			firstDate = _appState.CurrentUser.RegistrationDate;

			for (DateTime date = firstDate; date <= DateTime.Today; date = date.AddDays(1))
			{
				allDatesSinceReg.Add(date);
			}
		}

		protected override async Task OnInitializedAsync()
		{
			await UpdateDiaryComp();
		}

        public async Task UpdateDiaryComp()
        {
			await UpdateTables();
			await InvokeAsync(StateHasChanged);
		}

		protected virtual async Task UpdateTables()
		{
			allCols = await _diaryManager.GetDiaryCols(_appState.CurrentUser.Id, IsHabit);
			allPosts = await _diaryManager.GetDiaryPosts(_appState.CurrentUser.Id, IsHabit);
		}

		private async Task ToggleHabitValue(int colId, DateTime day)
		{
			await _diaryManager.ToggleHabitValue(colId, day);
			await _diarySubject.UpdateDiaryComponents();
		}

		protected async Task OnToggleHabitValue(int colId, DateTime day)
		{
			await _appState.ShowLoadingScreenWhileAwaiting(() => ToggleHabitValue(colId,day));
		}

		protected async Task RefreshDiaryComps()
		{
            await _appState.ShowLoadingScreenWhileAwaiting(_diarySubject.UpdateDiaryComponents);
        }
	}
}
