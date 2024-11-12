using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Diaries
{
	public enum DiaryColumnType {Habit, Words, NumberRange, Other}
	public enum DiaryTimeSpan{Week, Month, Year, All}
	public class Diary_log_column
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "You must set a name for the habit.")]
		public string Name { get; set; } = "";
		public DiaryColumnType Type { get; set; }
		public int Value_range_min { get; set; }
		public int Value_range_max { get; set; }
		public int Account_Id { get; set; }

		public bool IsSelected { get; set; } = true;
	}
}
