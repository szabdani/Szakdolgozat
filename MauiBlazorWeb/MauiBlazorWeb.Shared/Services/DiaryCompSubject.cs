using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Components.Widgets.Bases;
using MauiBlazorWeb.Shared.Interfaces;

namespace MauiBlazorWeb.Shared.Services
{
	public class DiaryCompSubject : IDiaryCompSubject
	{
		protected List<DiaryCompBase> allDiaryComps;

		public DiaryCompSubject()
		{
			allDiaryComps = new List<DiaryCompBase>();
		}

		public void Attach(DiaryCompBase diaryComp)
		{
			allDiaryComps.Add(diaryComp);
		}

		public void Detach(DiaryCompBase diaryComp)
		{
			allDiaryComps.Remove(diaryComp);
		}

		public async Task UpdateDiaryComponents()
		{
			foreach (var c in allDiaryComps)
			{
				await c.UpdateDiaryComp();
			}
		}
	}
}
