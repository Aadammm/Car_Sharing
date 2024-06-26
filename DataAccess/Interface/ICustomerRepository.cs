﻿
using Car_Sharing.Models;

namespace Car_Sharing.DataAccess.Interface
{
    public interface ICustomerRepository
    {
        public void AddEntity(Customer entity);

        public IEnumerable<Customer>? GetAll();

        public bool SaveChanges();

        public Customer? GetById(int id);

        public Customer? GetByName(string name);
    }
}
