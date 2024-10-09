using Car_Sharing.Data;
using Car_Sharing.DataAccess.Interface;
using Car_Sharing.Models;
using Car_Sharing.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Car_Sharing_Tests
{
    public class CarCarServiceTests
    {

        Mock<ICarRepository> mockRepository;
        CarService service;
        [SetUp]
        public void SetUp()
        {
            mockRepository = new Mock<ICarRepository>();
            service = new CarService(mockRepository.Object);
                                                          
        }
        [TestCase(1)]
        [TestCase(756)]
        [TestCase(15)]
        [TestCase(5)]
        public void GetById_ShouldReturnCar_IfCarExistInDatabase(int id )
        {
            var car = new Car()
            {
                Id=5,
                Name="car"
            };
    
        mockRepository.Setup(a=>a.GetById(It.Is<int>(id=>id>0))).Returns(car);
    
        var result = service.GetById(id);
    
        Assert.That(result, Is.EqualTo(car));
        }
        
        [TestCase(-1)]
        [TestCase(-756)]
        [TestCase(-15)]
        [TestCase(-5)]
        [TestCase(0)]
        public void GetById_ShouldReturnNull_IfCarNotExistInDatabase(int id )
        {
            mockRepository.Setup(a => a.GetById(It.Is<int>(id => id < 0))).Returns((Car)null);
        
            var result = service.GetById(id);
        
            Assert.That(result, Is.Null);
        }

        [TestCase("Skoda")]
        [TestCase("BMW M5")]
        [TestCase("Mitsubishi")]
        [TestCase("Ford")]
        public void GetByName_ShouldReturnCar_IfCarWithThisNameIsExistInDatabase(string name)
        {
            var car = new Car()
            {
                Name = name
            };
            mockRepository.Setup(c => c.GetByName(name)).Returns(car);

            var result = service.GetByName(name);

            Assert.That(result, Is.SameAs(car));
        }

        [TestCase("Skoda")]
        [TestCase("BMW M5")]
        [TestCase("Mitsubishi")]
        [TestCase("Ford")]
        public void GetByName_ShouldReturnNull_IfCarWithThisNameIsNotExistInDatabase(string name)
        {
            Car car = null;
            mockRepository.Setup(c => c.GetByName(name)).Returns((Car)null);

            var result = service.GetByName(name);

            Assert.That(result, Is.SameAs(car));
        }
        [Test]
        public void SaveChanges_ShouldReturnTrue_IfChangesWasSuccessful()
        {
            mockRepository.Setup(m => m.SaveChanges()).Returns(true);

            var result = service.SaveChanges();

            Assert.That(result, Is.True);
        }

        [Test]
        public void SaveChanges_ShouldReturnFalse_IfChangesWasNotSuccessful()
        {
            mockRepository.Setup(m => m.SaveChanges()).Returns(false);

            var result = service.SaveChanges();

            Assert.That(result, Is.False, "Save changes was not successful");
        }

        [Test]
        public void AllCarsWithCompany_ShouldReturnIEnumerableOfCars_IfExistsInDatabase()
        {
            //arrange
            var company = new Company()
            {
                Name = "testCompany",
                Id = 1
            };
            var cars = new List<Car>() {
            new Car()
            {
                Id = 1,
                Name = "Lambo"
            },new Car()
            {
                Id = 12,
                Name = "BMW"
            }   };
            mockRepository.Setup(m => m.GetAllCarsWithCompany(company)).Returns(cars);

            // Act
            var result = service.AllCarsWithCompany(company);

            // Assert
            CollectionAssert.AreEquivalent(result, cars);
        }

        [Test]
        public void AllCarsWithCompany_ShouldReturnNull_IfNotExistCompaniesInDatabase()
        {
            var company = new Company()
            {
                Name = "testCompany",
                Id = 1
            };

            mockRepository.Setup(m => m.GetAllCarsWithCompany(company)).Returns(new List<Car>());
            var result = service.AllCarsWithCompany(company);

            Assert.That(result, Is.Empty);
        }

        [TestCase("Skoda")]
        [TestCase("BMW M5")]
        [TestCase("Mitsubishi")]
        [TestCase("Ford")]
        public void CreateCar_ShouldReturnTrue_IfCarWasCreatedSuccessful(string name)
        {
            var company = new Company { Id = 1, Name = "testComapny" };
            mockRepository.Setup(m => m.GetByName(name)).Returns((Car)null);
            mockRepository.Setup(m => m.AddEntity(It.Is<Car>(c => c.Name == name)));
            mockRepository.Setup(m => m.SaveChanges()).Returns(true);

            var result = service.CreateAndSaveCar(company.Id, name);

            Assert.That(result, Is.True);
        }

        [TestCase("Skoda")]
        [TestCase("BMW M5")]
        [TestCase("Mitsubishi")]
        [TestCase("Ford")]
        public void CreateCar_ShouldReturnFalse_IfCarAlreadyExists(string name)
        {
            var company = new Company { Id = 1, Name = "testComapny" };
            var existingCar = new Car { Name = name };

            mockRepository.Setup(m => m.GetByName(name)).Returns(existingCar);

            var result = service.CreateAndSaveCar(company.Id, name);

            Assert.That(result, Is.False);
        }
    }
}
