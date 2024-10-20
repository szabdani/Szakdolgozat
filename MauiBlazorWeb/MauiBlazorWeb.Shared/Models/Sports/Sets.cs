using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Sports
{
    public class Sets
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Rpe { get; set; }
        public int Rep { get; set; }
        public int Weight { get; set; }
        public TimeSpan Length { get; set; }
        public int Exercise_Id { get; set; }
    }
}
