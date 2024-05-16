using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Company? CompanyCar { get; set; }
        public Customer? CustomerCar { get; set; }
        public int Company_Id { get; set; }

    }
}
