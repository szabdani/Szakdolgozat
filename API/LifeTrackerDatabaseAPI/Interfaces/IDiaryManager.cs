using LifeTrackerDatabaseAPI.Models;

namespace LifeTrackerDatabaseAPI.Intefaces
{
    public interface IDiaryManager
    {
        Task<bool> DeleteDiaryPost(int id);
        Task<bool> DeleteDiaryCol(int id);
        Task<bool> InsertDiaryPost(Diary_log_post newPost);
        Task<bool> InsertDiaryCol(Diary_log_column newCol);
        Task<bool> UpdateDiaryPost(Diary_log_post oldPost);
        Task<bool> UpdateDiaryCol(Diary_log_column oldCol);

        Task<List<Diary_log_column>> GetDiaryCols(int accountId, bool isHabit);
        Task<List<Diary_log_post>> GetDiaryPosts(int accountId, bool isHabit);
		Task<List<Diary_log_post>> GetDiaryColumnsPosts(int columnId);
	}
}