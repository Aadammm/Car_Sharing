using Car_Sharing.Models;
using Car_Sharing.Repositories;
using Car_Sharing.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Car_Sharing.ApiHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        readonly ICustomerRepository customerRepository;

        public CustomerController(IConfiguration config)
        {
            customerRepository = new CustomerRepository();
        }
        [HttpGet("GetCustomers")]
        public IEnumerable<Customer> GetCustomers()
        {
            return customerRepository.GetAll();
        }
        [HttpGet("GetCustomerById/{CustomerId}")]
        public ActionResult<Customer> GetCustomer(int CustomerId)
        {
            Customer? customer = customerRepository.GetById(CustomerId);
            if (customer != null)
            {
                return customerRepository.GetCustomerWithCar(customer);
            }
            throw new Exception("Failed to Find customer");
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

    }
}
