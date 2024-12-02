using MauiBlazorWeb.Shared.Services;

namespace MauiBlazorWeb.Shared.Interfaces
{
	public interface IObserverSubject
    {
        void Attach(ObserverComp observer);
        void Detach(ObserverComp observer);
		Task UpdateObservers();
    }
}