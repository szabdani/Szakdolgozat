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
        public async Task<List<Diary_log_column>> GetHabitCols(int accountId)
        {
            IDataAccess _data = new DataAccess();
            string sql = "select * from Diary_log_column where Account_id = @userid and type = 'Habit';";
            return await _data.LoadData<Diary_log_column, dynamic>(sql, new { @userid = accountId });
        }

        public async Task<bool> SetHabitCols(string habitName, int accountId)
        {
            IDataAccess _data = new DataAccess();
            string sql = "Insert into Diary_log_column (name, type, account_id) values (@name, 'Habit', @id);";
            int affectedRows = await _data.SaveData(sql, new { name = habitName, id = accountId });
            return affectedRows != 0;
        }

        public async Task<List<Diary_log_post>> GetDiaryPosts(int columnId)
        {
            IDataAccess _data = new DataAccess();
            string sql = "select * from Diary_log_post where Diary_log_column_id = @colid;";
            return await _data.LoadData<Diary_log_post, dynamic>(sql, new { @colid = columnId });
        }
    }
}
