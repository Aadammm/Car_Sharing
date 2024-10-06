using AutoMapper;
using Car_Sharing.Dtos;
using Car_Sharing.Models;
using Car_Sharing.DataAccess;
using Car_Sharing.DataAccess.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using Car_Sharing.New_Dto;

namespace Car_Sharing.ApiHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        readonly ICarRepository carRepository;
        readonly IMapper mapper;
        public CarController()
        {
            carRepository = new CarRepository();
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarDtoAdd, Car>();
                cfg.CreateMap<Car, CarDto>();
                cfg.CreateMap<Company, CompanyDto>();
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<Car, CarDtoWithCompanyAndCustomer>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));

            }));
        }

        [HttpGet("GetCars")]
        public IEnumerable<CarDto> GetCars()
        {
            var cars = carRepository.GetAll();
            var carsDto = cars.Select(x => mapper.Map<CarDto>(x));
            return carsDto;
        }
        [HttpGet("GetCarsWithCompanyAndCustomer")]
        public IEnumerable<CarDtoWithCompanyAndCustomer> GetCarsWithCompanyAndCustomer()
        {
            var cars = carRepository.GetAll();
             return cars.Select(x => mapper.Map<CarDtoWithCompanyAndCustomer>(x));
            
        }
        [HttpGet("GetCarById/{carId}")]
        public ActionResult<CarDtoWithCompanyAndCustomer> GetCarById(int carId)
        {
            Car? car = carRepository.GetById(carId);
            if (car is not null)
            {
                return mapper.Map<CarDtoWithCompanyAndCustomer>(car);
            }
            return NotFound("Car Not Found");
        }

        [HttpPut("EditCar")]
        public IActionResult EditCar(CarDto carEdit)
        {
            Car? car = carRepository.GetById(carEdit.Id);
            if (car is not null)
            {
                car.Name = carEdit.Name;
                car.Customer_Id = carEdit.Customer_Id;
                car.Company_Id = carEdit.Company_Id;
                if (carRepository.SaveChanges())
                {
                    return Ok();
                }
            }
            return NotFound("Car to update not found");
        }

        [HttpPost("AddCar")]
        public IActionResult AddCar(CarDtoAdd carDtoAdd)
        {
            Car car = mapper.Map<Car>(carDtoAdd);
            carRepository.AddEntity(car);
            if (carRepository.SaveChanges())
            {
                return Ok();
            }
            return StatusCode(500, "Failed Add Car");
        }

        [HttpDelete]
        public IActionResult Remove(int carId)
        {
            Car? car = carRepository.GetById(carId);
            if (car is not null)
            {
                carRepository.Remove(car);
                if (carRepository.SaveChanges())
                {
                    return Ok();
                }
            }
            return NotFound("Car to remove not found ");

        }

    }
}
