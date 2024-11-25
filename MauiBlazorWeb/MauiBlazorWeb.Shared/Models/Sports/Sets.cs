using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Sports
{
    public enum SetsType {Normal, Warmup, Drop }
	public class Sets
    {
        public int Id { get; set; }
		public bool IsDone { get; set; } = false;
		public SetsType Type { get; set; } = SetsType.Normal;
        public int? Reps { get; set; }
        public int? RPE { get; set; }
        public double? Weight { get; set; }
		public int LengthHours { get; set; }
		public int LengthMinutes{ get; set; } 
		public int LengthSeconds { get; set; } 
		public TimeSpan Length 
		{
			set 
			{
				LengthHours = value.Hours;
				LengthMinutes = value.Minutes;
				LengthSeconds = value.Seconds;
			}
		}
		public double? Distance { get; set; }
		public int Workoutindex { get; set; }
		public int Exercise_Id { get; set; }
		public int Workout_Id { get; set; }
	}
}
