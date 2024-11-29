using MauiBlazorWeb.Shared.Models.Diaries;

namespace MauiBlazorWeb.Shared.Interfaces
{
    public interface IDiaryAPIService
    {
        Task<bool> DeleteDiaryPost(Diary_log_post oldPost);
        Task<bool> DeleteDiaryCol(Diary_log_column oldCol);
        Task<bool> InsertDiaryPost(Diary_log_post newPost);
        Task<bool> InsertDiaryCol(Diary_log_column newCol);
        Task<bool> UpdateDiaryPost(Diary_log_post oldPost);
        Task<bool> UpdateDiaryCol(Diary_log_column oldCol);

        Task<List<Diary_log_column>> GetDiaryCols(int accountId, bool isHabit);
        Task<List<Diary_log_post>> GetDiaryPosts(int accountId, bool isHabit);
		Task<List<Diary_log_post>> GetDiaryColumnsPosts(int columnId);
	}
}