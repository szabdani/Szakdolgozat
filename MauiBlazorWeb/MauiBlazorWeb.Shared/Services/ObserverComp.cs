using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace MauiBlazorWeb.Shared.Services
{
	public class ObserverComp : ComponentBase, IDisposable
	{
		[Inject] protected ISubjectComp Subject { get; set; } = default!;

		protected override void OnInitialized()
		{
			base.OnInitialized();
			Subject.Attach(this);
		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await UpdateObserver();
		}

		public virtual void Dispose()
		{
			Subject.Detach(this);
		}

		public async Task UpdateObserver()
		{
			await UpdateTables();
			await InvokeAsync(StateHasChanged);
		}

		protected virtual async Task UpdateTables() { await Task.CompletedTask; }
	}
}
