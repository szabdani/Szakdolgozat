using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Sports
{
    public enum ExerciseType {Bodyweight, Machine, Timed, Distanced, Other}
    public class Exercise
    {
        public int Id { get; set; }
		public string Name { get; set; } = "";
		public string? Notes { get; set; }
		public TimeSpan Rest { get; set; }
		public ExerciseType Type { get; set; }
		public SportStatus Status { get; set; }
		public int Sport_Id { get; set; }
		public int? Creator_Account_Id { get; set; }
	}
}
