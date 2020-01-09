﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Winner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int year { get; set; }
        public string movie { get; set; }
        public string imgUrlActor { get; set; }
        public string imgUrlMovie { get; set; }
    }
}
