using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Sports
{
    public class Routine
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Notes { get; set; }
		public SportStatus Status { get; set; }
		public int Account_does_Sport_Id { get; set; }
	}
}
