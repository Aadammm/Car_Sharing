using AutoMapper;
using Car_Sharing.Data;
using Car_Sharing.Dtos;
using Car_Sharing.Models;
using Car_Sharing.DataAccess;
using Car_Sharing.DataAccess.Interface;
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
        IMapper mapper;

        public CompanyController(IConfiguration config)
        {
            companyRepository = new CompanyRepository();
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyAddDto, Company>();
                cfg.CreateMap<Car, CarBasicDto>();
                cfg.CreateMap<Company, CompanyWithCarsDto>()
                .ForMember(dest=>dest.CompanyCars,opt=>opt.MapFrom(src=>src.ListOfCompanyCars));
                cfg.CreateMap<Company,CompanyBasicDto >();
            }));
        }
        [HttpGet("GetCompanies")]
        public IEnumerable<CompanyBasicDto> GetCompanies()
        {
             var companies=companyRepository.GetAll();
            IEnumerable<CompanyBasicDto> companiesBasicDto =mapper.Map<IEnumerable<CompanyBasicDto>>(companies);
            return companiesBasicDto;
        }
        [HttpGet("GetCompanyById/{companyId}")]
        public IActionResult GetCompany(int companyId)
        {
            Company? company = companyRepository.GetById(companyId);
            if (company != null)
            {
                var companyWithCarsDto = mapper.Map<CompanyWithCarsDto>(company);   
                return Ok(companyWithCarsDto);
            }
            return NotFound("Company Not Found");
        }

        [HttpPut("EditCompany")]
        public IActionResult EditCompany(Company company)
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
            throw new Exception("Failed to Update Company");

        }
        [HttpPost("AddCompany")]
        public IActionResult AddCompany(CompanyAddDto companydto)
        {
            Company company = mapper.Map<Company>(companydto);
            companyRepository.AddEntity(company);
            if (companyRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed Add Company");
        }

    }
}
