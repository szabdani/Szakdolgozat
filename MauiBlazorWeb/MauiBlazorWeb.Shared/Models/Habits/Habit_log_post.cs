using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Habits
{
	public class Habit_log_post
	{
		public int Id { get; set; }
		public bool Status { get; set; }
		public int Habit_log_row_Id { get; set; }
		public int Habit_log_column_Id { get; set; }
	}
}
