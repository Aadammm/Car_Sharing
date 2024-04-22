using Car_Sharing.Data;
using Car_Sharing.Models;
using Car_Sharing.Repositories;
using Car_Sharing.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Car_Sharing.ApiHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        readonly ICompanyRepository companyRepository;

        public CompanyController(IConfiguration config)
        {
            companyRepository = new CompanyRepository();
        }
        [HttpGet("GetCompanies")]
        public IEnumerable<Company> GetCompanies()
        {
            return companyRepository.GetAll();
        }
        [HttpGet("GetCompanyById/{companyId}")]
        public Company GetCustomer(int companyId)
        {
            Company? company = companyRepository.GetById(companyId);
            if (company != null)
            {
                return company;
            }
            throw new Exception("Failed to Find customer");
        }

        [HttpPut("EditCompany")]
        public IActionResult EditCustomer(Company company)
        {
            Company? editcompany = companyRepository.GetById(company.Id);
            if (editcompany != null)
            {
                editcompany.Name = company.Name;
                if (companyRepository.SaveChanges())
                {
                    return Ok();
                }

            }
            throw new Exception("Failed to Update Customer");

        }

    }
}
