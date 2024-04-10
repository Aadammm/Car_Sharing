using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing.Data
{
    internal class CompanyRepository:ICompanyRepository
    {
        EntityFramework ef = new EntityFramework();
        public bool SaveChanges()
        {
            return ef.SaveChanges() > 0;
        }

        public IEnumerable<Company> GetCompanies()
        {
            return ef.Companies;
        }
        public IEnumerable<Car> GetCars()
        {
            return ef.Cars;
        }

        public Company GetSingleCompany(int id )
        {
            return ef.Companies.Where(c=>c.Id==id).FirstOrDefault();
        } 
        public Car GetSingleCar(int id)
        {
           return ef.Cars.Where(c => c.Id == id).FirstOrDefault();
        }

        public void AddEntity<T>(T entityAdd)
        {
            if(entityAdd!=null)
            ef.Add(entityAdd);
        }
    }
}
