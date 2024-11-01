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
        public DateOnly Date { get; set; }
        public string? Value { get; set; }
		public int Diary_log_column_Id { get; set; }
	}
}
