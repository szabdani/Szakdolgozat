using MauiBlazorWeb.Shared.Components.Widgets.Bases;
using MauiBlazorWeb.Shared.Services;

namespace MauiBlazorWeb.Shared.Interfaces
{
	public interface ISubjectComp
    {
        void Attach(ObserverComp observer);
        void Detach(ObserverComp observer);
		Task UpdateObservers();
    }
}