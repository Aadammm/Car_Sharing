using AutoMapper;
using Car_Sharing.Dtos;
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
    public class CarController : ControllerBase
    {
        readonly ICarRepository carRepository;
        readonly IMapper mapper;
        public CarController(IConfiguration config)
        {
            carRepository = new CarRepository();
            mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarDtoAdd, Car>(); 
                cfg.CreateMap<Car, CarBasicDto>();
                cfg.CreateMap<Company, CompanyBasicDto>();
                cfg.CreateMap<Car,CarWithCompanyDto>()
                .ForMember(dest=>dest.Company,opt=>opt.MapFrom(src=>src.CarCompany));

            }));
        }
        [HttpGet("GetCars")]
        public IEnumerable<CarBasicDto> GetCars()
        {
            var cars= carRepository.GetAll();
         
            IEnumerable<CarBasicDto> basicCars = mapper.Map<IEnumerable<CarBasicDto>>(cars);
            return basicCars;
        }
        [HttpGet("GetCarById/{carId}")]
        public ActionResult<CarWithCompanyDto> GetCar(int carId)
        {
            Car? car = carRepository.GetById(carId);
            if (car != null)
            {
                CarWithCompanyDto carBasic =mapper.Map<CarWithCompanyDto>(car);
                return carBasic;
            }
            throw new Exception("Failed to Find car");
        }

        [HttpPut("EditCar")]
        public IActionResult EditCar(Car car)
        {
            Car? editCar = carRepository.GetById(car.Id);
            if (editCar != null)
            {
                editCar.Name = car.Name;
                editCar.Company_Id = car.Company_Id;
                if (carRepository.SaveChanges())
                {
                    return Ok();
                }

            }
            throw new Exception("Failed to Update car");

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

            throw new Exception("Failed Add Car");
        }

    }
}
