using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Diaries
{
	public enum DiaryColumnType {Habit, Words, NumberRange, Other}
	public class Diary_log_column
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public DiaryColumnType Type { get; set; }
		public int Value_range_min { get; set; }
		public int Value_range_max { get; set; }
		public int Account_Id { get; set; }
	}
}
