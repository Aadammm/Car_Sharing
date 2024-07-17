
using System.Diagnostics.CodeAnalysis;
using Car_Sharing.Models;
using Car_Sharing.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Sharing.DataAccess
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public override Car? GetById(int id)
        {
            return _ef.Cars.Include(c => c.CompanyCar).SingleOrDefault(c => c.Id == id);
        }

        public Car? GetByName(string name)
        {
            return _ef.Cars.Where(a => a.Name == name).SingleOrDefault();
        }

        public IEnumerable<Car>? GetAllCarsWithCompany(Company? company)
        {
            if (company is not null)
            {
                return _ef.Cars.Include(car => car.CompanyCar).Where(c => c.Company_Id == company.Id).ToList();
            }
            return Enumerable.Empty<Car>().ToList();
        }
    }
}
