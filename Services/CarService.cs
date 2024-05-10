using Car_Sharing.Models;
using Car_Sharing.DataAccess;
using Car_Sharing.DataAccess.Interface;
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
        public CarService(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        public bool CreateAndSaveCar(int companyId, string name)
        {
            if (GetByName(name) == null)
            {
                carRepository.AddEntity(new Car()
                {
                    Name = name,
                    Company_Id = companyId
                });
                return SaveChanges();

            }
            return false;
        }

        public bool SaveChanges()
        {
            return carRepository.SaveChanges();
        }

        public Car GetByName(string name)
        {
            return carRepository.GetByName(name);
        }
        public Car GetById(int id)
        {
            return carRepository.GetById(id);
        }

        public IEnumerable<Car>? AllCarsWithCompany(Company company)
        {
            return carRepository.GetAllCarsWithCompany(company);
        }
    }
}
