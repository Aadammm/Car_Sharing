using AutoMapper;
using Car_Sharing.Data; 
using Car_Sharing.DataAccess.Interface;
using Microsoft.AspNetCore.Mvc;
using Car_Sharing.Dto;

namespace Car_Sharing.ApiHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        readonly ICompanyRepository companyRepository;
        readonly IMapper mapper;

        public CompanyController()
        {
            companyRepository = new CompanyRepository();
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyDtoAdd, Company>();
                cfg.CreateMap<Company, CompanyDto>();
                cfg.CreateMap<Company, CompanyWithCarsDto>()
                .ForMember(dest=>dest.Cars,opt=>opt.MapFrom(src=>src.Cars));
                cfg.CreateMap<Car, CarDto>();
            }));
        }
        [HttpGet("GetCompanies")]
        public IEnumerable<CompanyDto> GetCompanies()
        {
            var companies = companyRepository.GetAll();
            IEnumerable<CompanyDto> companiesBasicDto = mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesBasicDto;
        }

        [HttpGet("GetCompaniesWithCars")]
        public IEnumerable<CompanyWithCarsDto> GetCompaniesWithCars()
        {
            var companies = companyRepository.GetAll();
            IEnumerable<CompanyWithCarsDto> companiesBasicDto = mapper.Map<IEnumerable<CompanyWithCarsDto>>(companies);
            return companiesBasicDto;
        }

        [HttpGet("GetCompanyById/{companyId}")]
        public ActionResult<CompanyWithCarsDto> GetCompanyById(int companyId)
        {
            Company? company = companyRepository.GetById(companyId);
            if (company is not null)
            {
                var ccc = mapper.Map<CompanyWithCarsDto>(company);
                return ccc;


            }
            return NotFound("Company Not Found");
        }

        [HttpPut("EditCompany")]
        public IActionResult EditCompany(CompanyDto companyEdit)
        {
            Company? company = companyRepository.GetById(companyEdit.Id);
            if (company is not null)
            {
                company.Name = companyEdit.Name;
                if (companyRepository.SaveChanges())
                {
                    return Ok();
                }

            }
            return NotFound("Company Not Found");

        }

        [HttpPost("AddCompany")]
        public IActionResult AddCompany(CompanyDtoAdd companydtoAdd)
        {
            Company company = mapper.Map<Company>(companydtoAdd);
            companyRepository.AddEntity(company);
            if (companyRepository.SaveChanges())
            {
                return Ok();
            }
            return StatusCode(500, "Failed Add Company");
        }

        [HttpDelete]
        public IActionResult Remove(int companyId)
        {
            Company? company = companyRepository.GetById(companyId);
            if (company is not null)
            {
                companyRepository.Remove(company);
                if (companyRepository.SaveChanges())
                {
                    return Ok();
                }
            }
            return NotFound("Company to remove not found ");

        }
    }
}
