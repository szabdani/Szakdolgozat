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
        public string Name { get; set; }
        public int Sport_Id { get; set; }
        public int Routine_Id { get; set; }
    }
}
