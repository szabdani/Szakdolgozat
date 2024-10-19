using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Diaries
{
	public class Diary_log_post
	{
		public int Id { get; set; }
		public string value { get; set; }
		public int Diary_log_row_Id { get; set; }
		public int Diary_log_column_Id { get; set; }
	}
}
