using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Components.Pages;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models;
using MauiBlazorWeb.Shared.Models.Diaries;

namespace MauiBlazorWeb.Shared.Services
{
    public class DiaryAPIService(HttpClient httpClient) : IDiaryAPIService
    {
		public async Task<bool> InsertDiaryCol(Diary_log_column newCol)
        {
			var response = await httpClient.PostAsJsonAsync("api/Diary/InsertDiaryCol", newCol);
			return response.IsSuccessStatusCode;
		}
        public async Task<bool> UpdateDiaryCol(Diary_log_column oldCol)
        {
			var response = await httpClient.PutAsJsonAsync("api/Diary/UpdateDiaryCol", oldCol);
			return response.IsSuccessStatusCode;
		}
        public async Task<bool> DeleteDiaryCol(Diary_log_column oldCol)
        {
			var response = await httpClient.DeleteAsync($"api/Diary/DeleteDiaryCol/{oldCol.Id}");
			return response.IsSuccessStatusCode;
		}


        public async Task<bool> InsertDiaryPost(Diary_log_post newPost)
        {
			var response = await httpClient.PostAsJsonAsync("api/Diary/InsertDiaryPost", newPost);
			return response.IsSuccessStatusCode;
		}
        public async Task<bool> UpdateDiaryPost(Diary_log_post oldPost)
        {
			var response = await httpClient.PutAsJsonAsync("api/Diary/UpdateDiaryPost", oldPost);
			return response.IsSuccessStatusCode;
		}

        public async Task<bool> DeleteDiaryPost(Diary_log_post oldPost)
        {
			var response = await httpClient.DeleteAsync($"api/Diary/DeleteDiaryPost/{oldPost.Id}");
			return response.IsSuccessStatusCode;
		}


        public async Task<List<Diary_log_column>> GetDiaryCols(int accountId, bool isHabit)
        {
			var response = await httpClient.GetAsync($"api/Diary/GetDiaryCols?accountId={accountId}&isHabit={isHabit}");
			return await response.Content.ReadFromJsonAsync<List<Diary_log_column>>() ?? [];
		}
        public async Task<List<Diary_log_post>> GetDiaryColumnsPosts(int columnId)
        {
			var response = await httpClient.GetAsync($"api/Diary/GetDiaryColumnsPosts?columnId={columnId}");
			return await response.Content.ReadFromJsonAsync<List<Diary_log_post>>() ?? [];
		}
        public async Task<List<Diary_log_post>> GetDiaryPosts(int accountId, bool isHabit)
        {
			var response = await httpClient.GetAsync($"api/Diary/GetDiaryPosts?accountId={accountId}&isHabit={isHabit}");
			return await response.Content.ReadFromJsonAsync<List<Diary_log_post>>() ?? [];
		}
	}
}
