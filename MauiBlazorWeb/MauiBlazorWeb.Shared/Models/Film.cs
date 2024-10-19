using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Cim { get; set; }
        public int Ev { get; set; }
        public string Orszag { get; set; }
        public string Stilus { get; set; }
    }
}
