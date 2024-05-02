
using Car_Sharing.Models;

namespace Car_Sharing.Repositories.Interface
{
    internal interface ICarRepository
    {
        public void AddEntity(Car entity);

        public IEnumerable<Car> GetAll();

        public bool SaveChanges();

        public Car? GetById(int id);
        public void RemoveEntity(Car entity);
        public Car GetByName(string name);
        public List<Car>? GetCompanyCars(Company company);
        //public void LoadSingleReference(Car car);
    }
}
