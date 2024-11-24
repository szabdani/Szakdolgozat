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
		protected List<ObserverComp> allObservers;

		public SubjectComp()
		{
			allObservers = new List<ObserverComp>();
		}

		public void Attach(ObserverComp observer)
		{
			allObservers.Add(observer);
		}

		public void Detach(ObserverComp observer)
		{
			allObservers.Remove(observer);
		}

		public async Task UpdateObservers()
		{
			var snapshot= allObservers.ToList();
			foreach (var c in snapshot)
			{
				await c.UpdateObserver();
			}
		}
	}
}
