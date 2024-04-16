﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Models
{
    internal class Car
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Company? Company { get; set; }
        public Customer? Customer { get; set; }
        public int Company_Id { get; set; }

    }
}