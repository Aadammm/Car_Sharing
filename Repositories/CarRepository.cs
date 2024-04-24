
using System.Diagnostics.CodeAnalysis;
using Car_Sharing.Models;
using Car_Sharing.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Sharing.Repositories
{
    internal class CarRepository:BaseRepository<Car>, ICarRepository
    {
        [return: MaybeNull]
        public Car GetByName(string name)
        {
          Car? car=  _ef.Cars.Where(a => a.Name == name).SingleOrDefault();
            if(car!=null)
            {
                return car;
            }
            return null;
        }
        public List<Car>? GetCompanyCars(Company company)
        {
            return _ef.Cars.Include(car => car.CarCompany).Where(c => c.Company_Id == company.Id).ToList();
        }
        public void LoadSingleReference(Car car )
        {
            _ef.Entry(car).Reference(c=>c.CarCompany).Load();
        }

        public override Car? GetById(int id)
        {
            return _ef.Cars.Include(c => c.CarCompany).SingleOrDefault(c => c.Id == id);
        }




    }
}
