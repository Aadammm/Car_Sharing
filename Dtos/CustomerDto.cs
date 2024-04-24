using Car_Sharing.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public int? Rented_Car_Id { get; set; }
        public CarWithCompanyDto? CarWithCompanyDto { get; set; }

    }
}
