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
		public bool IsDone { get; set; }
		public SetsType Type { get; set; }
        public int? Reps { get; set; }
        public int? RPE { get; set; }
        public double? Weight { get; set; }
        public TimeSpan? Length { get; set; }
		public double? Distance { get; set; }
		public int Exercise_Id { get; set; }
		public int Workout_Id { get; set; }
	}
}
