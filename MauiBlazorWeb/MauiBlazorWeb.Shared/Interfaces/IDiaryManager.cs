using MauiBlazorWeb.Shared.Models.Diaries;

namespace MauiBlazorWeb.Shared.Interfaces
{
    public interface IDiaryManager
    {
        Task<List<Diary_log_post>> GetDiaryPosts(int columnId);
        Task<bool> SetHabitCols(string habitName, int accountId);
        Task<List<Diary_log_column>> GetHabitCols(int accountId);
    }
}