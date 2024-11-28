using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifeTrackerDatabaseAPI.Intefaces;
using LifeTrackerDatabaseAPI.Models;
using Microsoft.AspNetCore.Components;

namespace LifeTrackerDatabaseAPI.Services
{
    public class DiaryManager(IDataAccess data) : IDiaryManager
    {
		[Inject] protected IDataAccess Data { get; set; } = data;

		public async Task<bool> InsertDiaryCol(Diary_log_column newCol)
        {
            string sql = "Insert into Diary_log_column (name, type, Value_range_min, Value_range_max, account_id) values (@name, @type, @min, @max, @accountid);";
            int affectedRows = await Data.SaveData(sql, new { name = newCol.Name, type = newCol.Type.ToString(), min = newCol.Value_range_min, max = newCol.Value_range_max, accountid = newCol.Account_Id });
            return affectedRows != 0;
        }
        public async Task<bool> UpdateDiaryCol(Diary_log_column oldCol)
        {
            string sql = "Update Diary_log_column set name = @name, type = @type, Value_range_min = @min, Value_range_max = @max,Account_Id = @userid where id = @colid ;";
            int affectedRows = await Data.SaveData(sql, new { name = oldCol.Name, type = oldCol.Type.ToString(), min = oldCol.Value_range_min, max = oldCol.Value_range_max, userid = oldCol.Account_Id, colid = oldCol.Id });
            return affectedRows != 0;
        }
        public async Task<bool> DeleteDiaryCol(int id)
        {
            var posts = await GetDiaryColumnsPosts(id);
			foreach (var p in posts)
			{
				bool isCorrect = await DeleteDiaryPost(p.Id);
				if (!isCorrect)
					return isCorrect;
			}

			string sql = "Delete from Diary_log_column where id = @postid;";
            int affectedRows = await Data.SaveData(sql, new { postid = id });
            return affectedRows != 0;
        }


        public async Task<bool> InsertDiaryPost(Diary_log_post newPost)
        {
            string sql = "Insert into Diary_log_post (date, value, diary_log_column_id) values (@date, @value, @colid);";
            int affectedRows = await Data.SaveData(sql, new { date = newPost.Date.ToString("yyyy-MM-dd"), value = newPost.Value, colid = newPost.Diary_log_column_Id });
            return affectedRows != 0;
        }
        public async Task<bool> UpdateDiaryPost(Diary_log_post oldPost)
        {
            string sql = "Update Diary_log_post set date = @date, value = @value, Diary_log_column_Id = @colid where id = @postid;";
            int affectedRows = await Data.SaveData(sql, new { date = oldPost.Date.ToString("yyyy-MM-dd"), value = oldPost.Value, colid = oldPost.Diary_log_column_Id, postid = oldPost.Id });
            return affectedRows != 0;
        }

        public async Task<bool> DeleteDiaryPost(int id)
        {
            string sql = "Delete from Diary_log_post where id = @postid;";
            int affectedRows = await Data.SaveData(sql, new { postid = id });
            return affectedRows != 0;
        }


        public async Task<List<Diary_log_column>> GetDiaryCols(int accountId, bool isHabit)
        {
            string relation = isHabit ? "=" : "!=";
            string sql = $"Select * from Diary_log_column where Account_id = @userid and type {relation} 'Habit';";
            return await Data.LoadData<Diary_log_column, dynamic>(sql, new { @userid = accountId });
        }
        public async Task<List<Diary_log_post>> GetDiaryColumnsPosts(int columnId)
        {
            string sql = "Select * from Diary_log_post where Diary_log_column_id = @colid order by date;";
            return await Data.LoadData<Diary_log_post, dynamic>(sql, new { @colid = columnId });
        }
        public async Task<List<Diary_log_post>> GetDiaryPosts(int accountId, bool isHabit)
        {
            var retVal = new List<Diary_log_post>();

            var cols = await GetDiaryCols(accountId, isHabit);
            foreach (var col in cols)
                retVal.AddRange(await GetDiaryColumnsPosts(col.Id));

            return retVal;
        }
	}
}
