using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerDatabaseAPI.Models
{
	public class Diary_log_post
	{
		public int Id { get; set; }
        public DateTime Date { get; set; }
		public string Value { get; set; } = "";
		public int Diary_log_column_Id { get; set; }
	}
}
