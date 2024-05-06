using Car_Sharing.Models;
using Car_Sharing.Repositories;
using Car_Sharing.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Services
{
    public class CarService
    {
        readonly ICarRepository carRepository;
        public CarService()
        {
            carRepository = new CarRepository();
        }

        public bool CreateCar(int companyId, string name)
        {
            if (carRepository.GetByName(name) == null)
            {
                carRepository.AddEntity(new Car()
                {
                    Name = name,
                    Company_Id = companyId
                });
                return carRepository.SaveChanges();

            }
            return false;
        }

        public IEnumerable<Car>? AllCarsWithCompany(Company company)
        {
            return carRepository.GetAllCarsWithCompany(company);
        }
    }
}
