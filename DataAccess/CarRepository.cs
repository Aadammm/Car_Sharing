using Car_Sharing.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;
using Car_Sharing.Data;

namespace Car_Sharing.DataAccess
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public override IEnumerable<Car> GetAll()
        {
            return _ef.Cars.Include(c => c.Company).Include(c => c.Customer);
        }
        public override Car? GetById(int id)
        {
            return _ef.Cars.Include(c => c.Company).Include(c => c.Customer).SingleOrDefault(c => c.Id == id);
        }

        public Car? GetByName(string name)
        {
            return _ef.Cars.Where(a => a.Name == name).SingleOrDefault();
        }

        public IEnumerable<Car>? GetAllCarsWithCompany(Company? company)
        {
            if (company is not null)
            {
                return [.. _ef.Cars.Include(car => car.Company).Where(c => c.Company.Id == company.Id)];
            }
            return Enumerable.Empty<Car>().ToList();
        }
    }
}
