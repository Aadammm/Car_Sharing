using Car_Sharing.Data;
using Car_Sharing.DataAccess.Interface;

namespace Car_Sharing.Services
{
    public class CarService(ICarRepository carRepository)
    {
        readonly ICarRepository carRepository = carRepository;

        public bool CreateAndSaveCar(int companyId, string name)
        {
            if (GetByName(name) is null)
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
        public Car? GetByName(string name)
        {
            return carRepository.GetByName(name);
        }
        public Car? GetById(int id)
        {
            return carRepository.GetById(id);
        }

        public IEnumerable<Car>? AllCarsWithCompany(Company? company)
        {
            return carRepository.GetAllCarsWithCompany(company);
        }
    }
}
