using Car_Sharing.Data;

namespace Car_Sharing.DataAccess.Interface
{
    public interface ICarRepository
    {
        public void AddEntity(Car entity);

        public IEnumerable<Car> GetAll();

        public bool SaveChanges();

        public Car? GetById(int id);

        public Car? GetByName(string name);

        public IEnumerable<Car>? GetAllCarsWithCompany(Company? company);
        public void Remove(Car entity);
    }
}
