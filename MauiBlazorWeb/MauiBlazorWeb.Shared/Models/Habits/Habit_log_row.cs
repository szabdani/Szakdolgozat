using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Habits
{
	public class Habit_log_row
	{
		public int Id { get; set; }
		public DateOnly Date { get; set; }
		public int Account_Id { get; set; }
	}
}
