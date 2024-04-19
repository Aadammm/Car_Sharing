using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Car>? Cars { get; set; } = new List<Car>();

        public Company()
        {
            
        }
    }
}
