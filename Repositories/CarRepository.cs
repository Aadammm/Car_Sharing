using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.Data;
using Car_Sharing.Models;
using Car_Sharing.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Sharing.Repositories
{
    internal class CarRepository:BaseRepository<Car>, ICarRepository
    {
        public Car GetByName(string name)
        {
          Car? car=  _ef.Cars.Where(a => a.Name == name).SingleOrDefault();
            if(car!=null)
            {
                return car;
            }
            return null;
        }
        public Car? GetCarWithCompany(Car car)
        {
            return _ef.Cars.Include(car => car.Company).Where(c => c.Id == car.Id).FirstOrDefault();
        }
        public void LoadSingleReference(Car car )
        {
            _ef.Entry(car).Reference(c=>c.Company).Load();
        }



    }
}
