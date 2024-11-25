using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Components.Widgets.Bases
{
	public class TimeFilter
	{
		public enum TimePeriod { Week, Month, Year, All }

		public static bool FilterByTimePeriod(DateTime postDate, DateTime refDate, TimePeriod timeSpan)
		{
			return timeSpan switch
			{
				TimePeriod.Week => postDate > refDate.AddDays(-7),
				TimePeriod.Month => postDate.Month == refDate.Month && postDate.Year == refDate.Year,
				TimePeriod.Year => postDate.Year == refDate.Year,
				TimePeriod.All => true,
				_ => false,
			};
		}
	}
}
