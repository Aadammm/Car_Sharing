using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.Models;
using Microsoft.EntityFrameworkCore.Metadata;

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
        public void LoadSingleReference(Car car);
    }
}
