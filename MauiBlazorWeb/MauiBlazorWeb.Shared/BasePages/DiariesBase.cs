using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Components.DiaryComps;
using Microsoft.AspNetCore.Components;

namespace MauiBlazorWeb.Shared.BasePages
{
	public class DiariesBase : ComponentBase
	{
		protected List<DiaryCompBase> allDiaryComps;

		public bool Isit { get; set; } = false;

		public DiariesBase()
		{
			allDiaryComps = new List<DiaryCompBase>();
		}

		protected async Task UpdateDiaryComponents()
		{
			foreach (var c in allDiaryComps)
			{
				await c.UpdateDiaryComp();
			}

			await InvokeAsync(StateHasChanged);
		}
	}
}
