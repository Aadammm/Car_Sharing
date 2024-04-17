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
        private Car? _car;
        public Car? Car
        {
            get => _car;
            set
            {
                _car = value;
                if (_car != null && _car.Id != 0)
                {
                    Rented_Car_Id = _car.Id;
                }
                else
                {
                    Rented_Car_Id = null;
                }
            }
        }
    }
}

