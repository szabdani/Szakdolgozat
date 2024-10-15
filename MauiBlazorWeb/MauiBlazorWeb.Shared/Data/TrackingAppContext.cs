using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace MauiBlazorWeb.Shared.Data
{
    public class TrackingAppContext : IdentityDbContext<ApplicationUser>
	{
		public TrackingAppContext(DbContextOptions<TrackingAppContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Core Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Core Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}
		public DbSet<ApplicationUser> UserDatas { get; set; }
	}
}
