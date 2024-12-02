using MauiBlazorWeb.Shared.Interfaces;

namespace MauiBlazorWeb.Shared.Services
{
	public class ObserverSubject : IObserverSubject
	{
		protected List<ObserverComp> allObservers = [];
		public void Attach(ObserverComp observer) => allObservers.Add(observer);
		public void Detach(ObserverComp observer) => allObservers.Remove(observer);
		public async Task UpdateObservers()
		{
			var snapshot= allObservers.ToList();
			foreach (var c in snapshot)
			{
				await Task.Run(() => c.UpdateObserver());
			}
		}
	}
}
