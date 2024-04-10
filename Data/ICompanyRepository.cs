using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Data
{
    internal interface ICompanyRepository
    {
        public bool SaveChanges();

        public IEnumerable<Company> GetCompanies();
        public Company GetSingleCompany(int id);
        public void AddEntity<T>(T entityAdd);
        public IEnumerable<Car> GetCars();
        public Car GetSingleCar(int id);
       
    }
}
