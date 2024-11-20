using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Shared.Models.Sports;
using MauiBlazorWeb.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace MauiBlazorWeb.Shared.Components.Widgets.Bases
{
	public class SportCompBase : ObserverComp
	{
		[Inject] protected IAppState _appState { get; set; }
		[Inject] protected ISportManager _sportManager { get; set; }
		[Parameter] public Account_does_Sport CurrentAccountDoesSport { get; set; }

		protected List<MauiBlazorWeb.Shared.Models.Sports.Sport> allSports;

		protected DateTime firstDate;
		protected List<DateTime> allDatesSinceReg;
		
		public SportCompBase()
		{
			_appState = new AppState();
			_sportManager = new SportManager();

			CurrentAccountDoesSport = new Account_does_Sport();
			allSports = new List<MauiBlazorWeb.Shared.Models.Sports.Sport>();

			allDatesSinceReg = new List<DateTime>();
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			firstDate = _appState.CurrentUser.RegistrationDate;

			for (DateTime date = firstDate; date <= DateTime.Today; date = date.AddDays(1))
			{
				allDatesSinceReg.Add(date);
			}
		}

		public override async Task UpdateObserver()
		{
			await UpdateTables();
			await base.UpdateObserver();
		}

		protected virtual async Task UpdateTables()
		{
			allSports = await _sportManager.GetAllSports(_appState.CurrentUser.Id);
		}

		protected async Task RefreshSportComps()
		{
			await _appState.ShowLoadingScreenWhileAwaiting(_subject.UpdateObservers);
		}
	}
}
