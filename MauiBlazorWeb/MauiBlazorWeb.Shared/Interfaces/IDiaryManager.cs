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

        Task<List<Diary_log_post>> GetDiaryColumnsPosts(int columnId);
        Task<List<Diary_log_column>> GetHabitCols(int accountId);
    }
}