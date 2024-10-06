using AutoMapper;
using Car_Sharing.Dtos;
using Car_Sharing.Models;
using Car_Sharing.DataAccess;
using Car_Sharing.DataAccess.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Car_Sharing.New_Dto;

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
                cfg.CreateMap<Car, CarDto>();
                cfg.CreateMap<CustomerAddDto, Customer>();
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<Customer, CustomerDtoWithCar>()
                .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car));
            }));
        }

        [HttpGet("GetCustomers")]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customers = customerRepository.GetAll();
                       return mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        [HttpGet("GetCustomersWithCar")]
        public IEnumerable<CustomerDtoWithCar> GetCustomersWithCar()
        {
            var customers = customerRepository.GetAll();
            return customers.Select(c => mapper.Map<CustomerDtoWithCar>(c));
        }

        [HttpGet("GetCustomerById/{CustomerId}")]
        public ActionResult<CustomerDtoWithCar> GetCustomer(int CustomerId)
        {
            var customer = customerRepository.GetById(CustomerId);
            if (customer is not null)
            {
                CustomerDtoWithCar customerDto = mapper.Map<CustomerDtoWithCar>(customer);
                return customerDto;
            }
            return NotFound("Custumer Not Found");

        }

        [HttpPut("EditCustomer")]
        public IActionResult EditCustomer(CustomerDtoEdit customer)
        {
            Customer? customerdb = customerRepository.GetById(customer.Id);
            if (customerdb is not null)
            {
                customerdb.Name = customer.Name;
                customerdb.Rented_Car_Id = customer.Rented_Car_Id;
                if (customerRepository.SaveChanges())
                {
                    return Ok();
                }
            }
            return NotFound("Customer to edit not found");
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

        [HttpDelete]
        public IActionResult Remove(int customerId)
        {
            Customer? customer = customerRepository.GetById(customerId);
            if (customer is not null)
            {
                customerRepository.Remove(customer);
                if (customerRepository.SaveChanges())
                {
                    return Ok();
                }
            }
            return NotFound("Customer to remove not found ");

        }
    }
}
