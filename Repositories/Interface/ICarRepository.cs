using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
