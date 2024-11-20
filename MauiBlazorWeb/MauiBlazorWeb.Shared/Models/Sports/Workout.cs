using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Sports
{
    public class Workout
    {
        public int Id { get; set; }
        public bool IsDone { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Finishtime { get; set; }
        public string? Notes { get; set; }
		public bool IsRoutineExample { get; set; }
		public int Routine_Id { get; set; }
        public int Account_does_Sport_Id {  get; set; }
    }
}
