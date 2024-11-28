using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeTrackerDatabaseAPI.Models
{
	public class Routine
	{
		public int Id { get; set; }
		public SportStatus Status { get; set; }
		public int ExampleWorkout_Id { get; set; }
		public int Account_does_Sport_Id { get; set; }
	}
}
