using AutoMapper;
using Car_Sharing.Dtos;
using Car_Sharing.Models;
using Car_Sharing.DataAccess;
using Car_Sharing.DataAccess.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Car_Sharing.ApiHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerRepository customerRepository;
        readonly IMapper mapper;
        public CustomerController()
        {
            customerRepository = new CustomerRepository();
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Company, CompanyBasicDto>();
                cfg.CreateMap<CustomerAddDto, Customer>();
                cfg.CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.CarWithCompanyDto, opt => opt.MapFrom(src => src.Car));
                cfg.CreateMap<Car, CarWithCompanyReferenceDto>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.CompanyCar));

            }));
        }
        [HttpGet("GetCustomers")]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customers = customerRepository.GetAll();

            return mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        [HttpGet("GetCustomerById/{CustomerId}")]
        public ActionResult<CustomerDto> GetCustomer(int CustomerId)
        {
            var customer = customerRepository.GetById(CustomerId);
            if (customer != null)
            {
                CustomerDto customerDto = mapper.Map<CustomerDto>(customer);

                return customerDto;
            }
            return NotFound("Custumer Not Found");

        }

        [HttpPut("EditCustomer")]
        public IActionResult EditCustomer(Customer customer)
        {
            Customer? customerdb = customerRepository.GetById(customer.Id);
            if (customerdb != null)
            {
                customerdb.Name = customer.Name;
                customerdb.Rented_Car_Id = customer.Rented_Car_Id;
                if (customerRepository.SaveChanges())
                {
                    return Ok();
                }

            }
            return StatusCode(500, "Failed to Update Customer");
        }
        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer(CustomerAddDto customer)
        {
            Customer userdb = mapper.Map<Customer>(customer);
            customerRepository.AddEntity(userdb);
            if (customerRepository.SaveChanges())
            {
                return Ok();
            }
            return StatusCode(500, "Failed Add User");
        }
    }
}
