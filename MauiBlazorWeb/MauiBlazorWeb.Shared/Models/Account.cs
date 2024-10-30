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

        [Required(ErrorMessage = "You must set a username for your account.")]
        [MaxLength(45, ErrorMessage = "Username cannot exceed 45 characters.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "You must set a valid email address for your account.")]
        [EmailAddress(ErrorMessage = "You must set a valid email address for your account.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "You must set a password for your account.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
		public string? Password1 { get; set; }

        [Required(ErrorMessage = "You must repeat your password for confirmation.")]
        [Compare("Password1", ErrorMessage = "The password are not matching.")]
        public string? Password2 { get; set; }

        public byte[]? Password_hash { get; set; }
        public byte[]? Password_salt { get; set; }

        public DateTime Birthdate { get; set; } = new DateTime(2000,1,1);

		public GenderType Gender { get; set; }

		public bool Admin { get; set; } = false;

        public string? PfpPath { get; set; }

        public string? LoginUsername { get; set; }
        public string? LoginPassword { get; set; }
    }
}
