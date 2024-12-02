using MauiBlazorWeb.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace MauiBlazorWeb.Shared.Services
{
	public class ObserverComp : ComponentBase, IDisposable
	{
		[Inject] protected IObserverSubject Subject { get; set; } = default!;

		protected override void OnInitialized() => Subject.Attach(this);
		protected override async Task OnInitializedAsync() => await UpdateObserver();
		public virtual void Dispose() => Subject.Detach(this);
		protected virtual async Task UpdateTables() => await Task.CompletedTask;
		public async Task UpdateObserver()
		{
			await UpdateTables();
			await InvokeAsync(StateHasChanged);
		}
	}
}
