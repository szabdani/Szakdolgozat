using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Components.Pages;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models.Diaries;

namespace MauiBlazorWeb.Shared.Services
{
    public class DiaryManager : IDiaryManager
    {
        public async Task<bool> InsertDiaryCols(Diary_log_column newCol)
        {
            IDataAccess _data = new DataAccess();
            string sql = "Insert into Diary_log_column (name, type, Value_range_min, Value_range_max, account_id) values (@name, @type, @min, @max, @accountid);";
            int affectedRows = await _data.SaveData(sql, new { name = newCol.Name, type = newCol.Type.ToString(), min = newCol.Value_range_min, max = newCol.Value_range_max, accountid = newCol.Account_Id });
            return affectedRows != 0;
        }
        public async Task<bool> UpdateDiaryCols(Diary_log_column oldCol)
        {
            IDataAccess _data = new DataAccess();
            string sql = "Update Diary_log_column set name = @name, type = @type, Value_range_min = @min, Value_range_max = @max,Account_Id = @userid where id = @colid ;";
            int affectedRows = await _data.SaveData(sql, new { name = oldCol.Name, type = oldCol.Type.ToString(), min = oldCol.Value_range_min, max = oldCol.Value_range_max, userid = oldCol.Account_Id, colid = oldCol.Id });
            return affectedRows != 0;
        }
        public async Task<bool> DeleteDiaryCols(Diary_log_column oldCol)
        {
            IDataAccess _data = new DataAccess();
            string sql = "Delete from Diary_log_column where id = @postid;";
            int affectedRows = await _data.SaveData(sql, new { postid = oldCol.Id });
            return affectedRows != 0;
        }
        public async Task<bool> InsertDiaryPost(Diary_log_post newPost)
        {
            IDataAccess _data = new DataAccess();
            string sql = "Insert into Diary_log_post (date, value, diary_log_column_id) values (@date, @value, @colid);";
            int affectedRows = await _data.SaveData(sql, new { date = newPost.Date.ToString("yyyy-MM-dd"), value = newPost.Value, colid = newPost.Diary_log_column_Id });
            return affectedRows != 0;
        }
        public async Task<bool> UpdateDiaryPost(Diary_log_post oldPost)
        {
            IDataAccess _data = new DataAccess();
            string sql = "Update Diary_log_post set date = @date, value = @value, Diary_log_column_Id = @colid where id = @postid;";
            int affectedRows = await _data.SaveData(sql, new { date = oldPost.Date.ToString("yyyy-MM-dd"), value = oldPost.Value, colid = oldPost.Diary_log_column_Id, postid = oldPost.Id });
            return affectedRows != 0;
        }

        public async Task<bool> DeleteDiaryPost(Diary_log_post oldPost)
        {
            IDataAccess _data = new DataAccess();
            string sql = "Delete from Diary_log_post where id = @postid;";
            int affectedRows = await _data.SaveData(sql, new { postid = oldPost.Id });
            return affectedRows != 0;
        }

        public async Task<List<Diary_log_column>> GetDiaryCols(int accountId, bool isHabit)
        {
            IDataAccess _data = new DataAccess();

            string relation = isHabit ? "=" : "!=";
            string sql = $"Select * from Diary_log_column where Account_id = @userid and type {relation} 'Habit';";
            return await _data.LoadData<Diary_log_column, dynamic>(sql, new { @userid = accountId });
        }

        public async Task<List<Diary_log_post>> GetDiaryColumnsPosts(int columnId)
        {
            IDataAccess _data = new DataAccess();
            string sql = "Select * from Diary_log_post where Diary_log_column_id = @colid order by date;";
            return await _data.LoadData<Diary_log_post, dynamic>(sql, new { @colid = columnId });
        }
        public async Task<List<Diary_log_post>> GetDiaryPosts(int accountId, bool isHabit)
        {
            IDataAccess _data = new DataAccess();
            List<Diary_log_post> retVal = new List<Diary_log_post>();

            var cols = await GetDiaryCols(accountId, isHabit);
            foreach (var col in cols)
                retVal.AddRange(await GetDiaryColumnsPosts(col.Id));

            return retVal;
        }

		public async Task DeleteSameDatePosts(int accountId, DateTime date, bool isHabit)
		{
			var posts = await GetDiaryPosts(accountId, isHabit);
			foreach (var p in posts.Where(p => p.Date == date))
			{
				bool isCorrect = await DeleteDiaryPost(p);
				if (!isCorrect)
					throw new Exception($"Sorry, we could not delete post.");
			}
		}

		public async Task ToggleHabitValue(int colId, DateTime date)
		{
            bool isCorrect = true;

			var posts = await GetDiaryColumnsPosts(colId);
            var post = posts.FirstOrDefault(p => p.Date == date);
			if (post == null)
			{
				post = new Diary_log_post { Date = date, Value = "X", Diary_log_column_Id = colId };
				isCorrect = await InsertDiaryPost(post);
			}
			else
			{
				post.Value = post.Value == "X" ? "" : "X";
				isCorrect = await UpdateDiaryPost(post);
			}

			if (!isCorrect)
				throw new Exception($"Sorry, we could not toggle the value.");
		}
	}
}
