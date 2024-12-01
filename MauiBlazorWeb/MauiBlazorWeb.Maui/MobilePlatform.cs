using MauiBlazorWeb.Shared.Interfaces;

namespace MauiBlazorWeb.Maui
{
	public class MobilePlatform : IPlatformService
	{
		public string GetPlatform() => "MOBILE";
	}
}
