using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiBlazorWeb.Shared.Interfaces;

namespace MauiBlazorWeb.Shared.Models
{
    public enum GenderType { Male, Female, Other }
    public class Account
	{
        public int Id { get; set; }

        public string Username { get; set; } = "Account";

        public string Email { get; set; } = "";

        public byte[] Password_hash { get; set; } = new byte[32];
        public byte[] Password_salt { get; set; } = new byte[16];

        public DateTime Birthdate { get; set; }
		
		public GenderType Gender { get; set; }
        public DateTime RegistrationDate { get; set; }

        public bool Admin { get; set; } = false;
    }
}
