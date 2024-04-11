using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Sharing.Models;
using Car_Sharing.Repositories;
using Car_Sharing.Repositories.Interface;

namespace Car_Sharing.Data
{
    internal class CompanyRepository :BaseRepository<Company>, ICompanyRepository
    {
    }
}
