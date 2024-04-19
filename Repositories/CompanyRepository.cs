using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.Models;
using Car_Sharing.Repositories;
using Car_Sharing.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Car_Sharing.Data
{
    internal class CompanyRepository :BaseRepository<Company>, ICompanyRepository
    {
        [return: MaybeNull]
        public Company GetByName(string name)
        {
            Company? company = _ef.Companies.Where(a => a.Name == name).SingleOrDefault();
            if (company != null)
            {
                return company;
            }
            return null;
        }
        public Company? GetCompanyWithCars(Company company)
        {
            return _ef.Companies.Include(c => c.Cars).Where(c => c.Id == company.Id).FirstOrDefault();
        }
        public void LoadAllReferences(Company company)
        {
            _ef.Entry(company).Collection(c => c.Cars).Load();
        }
    }
}
