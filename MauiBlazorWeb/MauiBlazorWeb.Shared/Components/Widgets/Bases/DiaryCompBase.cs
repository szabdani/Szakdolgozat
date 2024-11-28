using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models.Diaries;
using MauiBlazorWeb.Shared.Services;
using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MauiBlazorWeb.Shared.Components.Widgets.Bases
{
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

		protected override async Task UpdateTables()
		{
			await base.UpdateTables();
			allCols = await DiaryManager.GetDiaryCols(AppState.CurrentUser.Id, IsHabit);
			allPosts = await DiaryManager.GetDiaryPosts(AppState.CurrentUser.Id, IsHabit);
		}

		private async Task ToggleHabitValue(int colId, DateTime day)
		{
			bool isCorrect = true;

			var posts = await DiaryManager.GetDiaryColumnsPosts(colId);
			var post = posts.FirstOrDefault(p => p.Date == day);
			if (post == null)
			{
				post = new Diary_log_post { Date = day, Value = "X", Diary_log_column_Id = colId };
				isCorrect = await DiaryManager.InsertDiaryPost(post);
			}
			else
			{
				post.Value = post.Value == "X" ? "" : "X";
				isCorrect = await DiaryManager.UpdateDiaryPost(post);
			}

			if (!isCorrect)
				throw new Exception($"Sorry, we could not toggle the value.");
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
	}
}
