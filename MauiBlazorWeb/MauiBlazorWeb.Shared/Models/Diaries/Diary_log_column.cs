using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Diaries
{
	public class Diary_log_column
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public int Value_range { get; set; }
		public int Account_Id { get; set; }
	}
}
