using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Data;
using Microsoft.AspNetCore.Components;


namespace MauiBlazorWeb.Shared.Controllers
{
	// This class creates a controller that allows us to query the database for pizza specials and returns them as JSON at the (http://localhost:5000/specials) URL.
	[Route("specials")]
	//[ApiController]
	public class TrackingAppController
	{
		private readonly TrackingAppContext _db;

		public TrackingAppController(TrackingAppContext db)
		{
			_db = db;
		}

		/*
		[HttpGet]
		public async Task<ActionResult<List<UserData>>> GetSpecials()
		{
			return (await _db.UserDatas.ToListAsync()).OrderByDescending(s => s.BasePrice).ToList();
		}
		*/
	}
}
