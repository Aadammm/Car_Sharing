using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Models
{
    internal class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Rented_Car_Id { get; set; }
        public Car? Car { get; set; }
    }
}
