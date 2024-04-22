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

        public CarController(IConfiguration config)
        {
            carRepository = new CarRepository();
        }
        [HttpGet("GetCars")]
        public IEnumerable<Car> GetCars()
        {
            return carRepository.GetAll();
        }
        [HttpGet("GetCarById/{carId}")]
        public ActionResult<Car> GetCar(int carId)
        {
            Car? car = carRepository.GetById(carId);
            if (car != null)
            {
                return car;
            }
            throw new Exception("Failed to Find customer");
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
            throw new Exception("Failed to Update Customer");

        }

    }
}
