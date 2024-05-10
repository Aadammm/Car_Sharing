using AutoMapper;
using Car_Sharing.Dtos;
using Car_Sharing.Models;
using Car_Sharing.DataAccess;
using Car_Sharing.DataAccess.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models;
using System.Diagnostics.CodeAnalysis;

namespace Car_Sharing.ApiHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerRepository customerRepository;
        IMapper mapper;
        public CustomerController(IConfiguration config)
        {
            customerRepository = new CustomerRepository();
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Company,CompanyBasicDto>();
                cfg.CreateMap<CustomerAddDto,Customer>();
                cfg.CreateMap<Customer,CustomerDto>()
                .ForMember(dest => dest.CarWithCompanyDto, opt => opt.MapFrom(src => src.Car));
                cfg.CreateMap<Car, CarWithCompanyDto>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.CarCompany));

            }));
        }
        [HttpGet("GetCustomers")]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customers= customerRepository.GetAll();
           
           return  mapper.Map<IEnumerable<CustomerDto>>(customers);
        }
             
        [HttpGet("GetCustomerById/{CustomerId}")]
        public CustomerDto GetCustomer(int CustomerId)
        {
            var customer = customerRepository.GetById(CustomerId);
            if (customer != null)
            {
                CustomerDto customerDto = mapper.Map<CustomerDto>(customer);
                
                return customerDto;
            }
            throw new Exception("Failed to Find customer");//404 returnut
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
            throw new Exception("Failed to Update Customer");
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
            throw new Exception("Failed Add User");
        }
    }
}
