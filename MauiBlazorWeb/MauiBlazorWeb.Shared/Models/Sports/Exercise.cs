﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBlazorWeb.Shared.Models.Sports
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Rest { get; set; }
        public int Routine_Id { get; set; }
    }
}
