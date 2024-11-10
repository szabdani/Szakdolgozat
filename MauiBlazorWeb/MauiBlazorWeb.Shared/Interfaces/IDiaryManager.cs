using MauiBlazorWeb.Shared.Models.Diaries;

namespace MauiBlazorWeb.Shared.Interfaces
{
    public interface IDiaryManager
    {
        Task<bool> DeleteDiaryPost(Diary_log_post oldPost);
        Task<bool> DeleteDiaryCols(Diary_log_column oldCol);
        Task<bool> InsertDiaryPost(Diary_log_post newPost);
        Task<bool> InsertDiaryCols(Diary_log_column newCol);
        Task<bool> UpdateDiaryPost(Diary_log_post oldPost);
        Task<bool> UpdateDiaryCols(Diary_log_column oldCol);

        Task<List<Diary_log_column>> GetDiaryCols(int accountId, bool isHabit);
        Task<List<Diary_log_post>> GetDiaryPosts(int accountId, bool isHabit);

        Task<List<DateTime>> GetUniquePostDates(int accountId, bool isHabit);
		Task<List<Diary_log_post>> GetDiaryColumnsPosts(int columnId);

		Task DeleteSameDatePosts(int accountId, DateTime date, bool isHabit);

		Task ToggleHabitValue(int colId, DateTime date);
	}
}