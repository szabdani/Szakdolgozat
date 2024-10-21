using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Sports
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
		public bool IsShared { get; set; }
		public int Creator_Account_Id { get; set; }
	}
}
