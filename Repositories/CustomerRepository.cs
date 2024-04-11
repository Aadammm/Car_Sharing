﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.Models;
using Car_Sharing.Repositories.Interface;

namespace Car_Sharing.Repositories
{
    internal class CustomerRepository:BaseRepository<Customer>,ICustomerRepository
    {
    }
}
