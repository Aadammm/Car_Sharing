using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.Dtos;
using Car_Sharing.Models;
using Microsoft.EntityFrameworkCore;

namespace Car_Sharing.Repositories.Interface
{
    internal interface ICustomerRepository
    {
        public void AddEntity(Customer entity);

        public IEnumerable<Customer> GetAll();

        public bool SaveChanges();

        public Customer GetById(int id);

        public void RemoveEntity(Customer entity);
        public Customer GetByName(string name);
        public void LoadSingleReference(Customer customer);

    }
}
