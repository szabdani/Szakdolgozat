using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models.Diaries;
using MauiBlazorWeb.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace MauiBlazorWeb.Shared.Components.Widgets.Bases
{
	public class DiaryCompBase : ObserverComp
	{
		[Inject] protected IAppState AppState { get; set; } = default!;
		[Inject] protected IDiaryAPIService DiaryAPI { get; set; } = default!;
		[Parameter] public bool IsHabit { get; set; }

		protected static List<Diary_log_column> allDiaryCols = [];
		protected static List<Diary_log_post> allDiaryPosts = [];
		protected static List<Diary_log_column> allHabitCols = [];
		protected static List<Diary_log_post> allHabitPosts = [];

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

		protected static async Task UpdateAllTables(IAppState appState, IDiaryAPIService diaryAPI)
		{
			allDiaryCols = await diaryAPI.GetDiaryCols(appState.CurrentUser.Id, false);
			allDiaryPosts = await diaryAPI.GetDiaryPosts(appState.CurrentUser.Id, false);

			allHabitCols = await diaryAPI.GetDiaryCols(appState.CurrentUser.Id, true);
			allHabitPosts = await diaryAPI.GetDiaryPosts(appState.CurrentUser.Id, true);
		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await UpdateAllTables(AppState, DiaryAPI);
		}

		private async Task ToggleHabitValue(int colId, DateTime day)
		{
			bool isCorrect = true;

			var posts = await DiaryAPI.GetDiaryColumnsPosts(colId);
			var post = posts.FirstOrDefault(p => p.Date == day);
			if (post == null)
			{
				post = new Diary_log_post { Date = day, Value = "X", Diary_log_column_Id = colId };
				isCorrect = await DiaryAPI.InsertDiaryPost(post);
			}
			else
			{
				post.Value = post.Value == "X" ? "" : "X";
				isCorrect = await DiaryAPI.UpdateDiaryPost(post);
			}

			if (!isCorrect)
				throw new Exception($"Sorry, we could not toggle the value.");
			await Task.Run(RefreshDiaryComps);
		}

		protected async Task OnToggleHabitValue(int colId, DateTime day)
		{
			await AppState.ShowLoadingScreenWhileAwaiting(() => ToggleHabitValue(colId, day));
		}

		protected async Task RefreshDiaryComps()
		{
			await UpdateAllTables(AppState, DiaryAPI);
			await AppState.ShowLoadingScreenWhileAwaiting(Subject.UpdateObservers);
		}
	}
}
