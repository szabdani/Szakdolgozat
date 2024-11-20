using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Sports
{
	public enum SportStatus { Public, Private, Deleted}
	public class Sport
    {
        public int Id { get; set; }
		public string Name { get; set; } = "";
		public SportStatus Status { get; set; }
		public int Creator_Account_Id { get; set; }
	}
}
