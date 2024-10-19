using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models
{
	public class Account
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password_hash { get; set; }
		public string Password_salt{ get; set; }
		public int Age { get; set; }
		public int Sex { get; set; }
		public bool Admin { get; set; } = false;
	}
}
