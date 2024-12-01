using MauiBlazorWeb.Shared.Interfaces;

namespace MauiBlazorWeb.Web
{
	public class WebPlatform : IPlatformService
	{
		public string GetPlatform() => "WEB";
	}
}
