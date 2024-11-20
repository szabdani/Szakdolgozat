using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Components.Widgets.Bases;
using MauiBlazorWeb.Shared.Interfaces;

namespace MauiBlazorWeb.Shared.Services
{
	public class SubjectComp : ISubjectComp
	{
		protected List<ObserverComp> allDiaryComps;

		public SubjectComp()
		{
			allDiaryComps = new List<ObserverComp>();
		}

		public void Attach(ObserverComp observer)
		{
			allDiaryComps.Add(observer);
		}

		public void Detach(ObserverComp observer)
		{
			allDiaryComps.Remove(observer);
		}

		public async Task UpdateObservers()
		{
			foreach (var c in allDiaryComps)
			{
				await c.UpdateObserver();
			}
		}
	}
}
