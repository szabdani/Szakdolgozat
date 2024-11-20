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
		[Inject] protected ISubjectComp _subject { get; set; }
		
		public ObserverComp()
		{
			_subject = new SubjectComp();
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();
			_subject.Attach(this);
		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			await UpdateObserver();
		}

		public void Dispose()
		{
			_subject.Detach(this);
		}

		public virtual async Task UpdateObserver()
		{
			await InvokeAsync(StateHasChanged);
		}
	}
}
