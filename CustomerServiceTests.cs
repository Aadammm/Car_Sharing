using Car_Sharing.Services;
using Car_Sharing.DataAccess.Interface;
using Moq;
using NUnit.Framework;
using Car_Sharing.Models;
using NUnit.Framework.Legacy;
using Microsoft.IdentityModel.Tokens;
using System.Formats.Asn1;
using Castle.Core.Resource;

namespace Car_Sharing_Tests
{
    public class CustomerServiceTests
    {
        Mock<ICustomerRepository> mockRepository;
        CustomerService service;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new Mock<ICustomerRepository>();//musi sa vytvorit mock na fake pracu s repository(praca s databazou)
            service = new CustomerService(mockRepository.Object);//tato trieda sa testuje tak nemoze byt mockovana, ale jej zavislosti musia byt 
                                                                 //CustomerService ma v konstruktore ako parameter CustomerRepository kvoli Dependency Inversion 
        }

        [Test]
        public void GetCustomers_ShouldReturnIEnumerableOfCustomers_IfExistsInDatabase()
        {
            //arrange
            var customers = new List<Customer>() {
            new Customer()
            {
                Id = 1,
                Name = "Adam"
            },new Customer()
            {
                Id = 12,
                Name = ""
            }   };
            mockRepository.Setup(m => m.GetAll()).Returns(customers);

            // Act
            var result = service.GetCustomers();
            // Assert

            //Assert.That(result.Count(), Is.EqualTo(abc.Count));
            CollectionAssert.AreEquivalent(result, customers);//porovna itemy v kolekciach(nemusia byt v poradi)
        }
        [Test]
        public void GetCustomers_ShouldReturnEmptyEnumerable_IfNotExistCustomersInDatabase()
        {
            mockRepository.Setup(m => m.GetAll()).Returns(Enumerable.Empty<Customer>());

            var result = service.GetCustomers();

            Assert.That(result, Is.Empty);
        }
        [Test]
        public void SaveChanges_ShouldReturnTrue_IfChangesWasSuccess()
        {
            mockRepository.Setup(m => m.SaveChanges()).Returns(true);

            var result = service.SaveChange();

            Assert.That(result, Is.True);
        }

        [Test]
        public void SaveChanges_ShouldReturnFalse_IfChangesWasNotSuccess()
        {

            mockRepository.Setup(m => m.SaveChanges()).Returns(false);

            var result = service.SaveChange();

            Assert.That(result, Is.False, "skoda");
        }
        [TestCase("Adam")]
        [TestCase("Jozo")]
        [TestCase("Anastazia")]
        [TestCase("Eva")]

        public void GetByName_ShouldReturnNull_IfCustomerWithThisNameExistIsNotInDatabase(string name)
        {
            Customer customer = null;
            mockRepository.Setup(m => m.GetByName(name)).Returns(() => null);

            var result = service.GetByName(name);

            Assert.That(result, Is.SameAs(customer));
        }

        [TestCase("Adam")]
        [TestCase("Jozo")]
        [TestCase("Anastazia")]
        [TestCase("Eva")]
        public void GetByName_ShouldReturnCustomer_IfCustomerWithThisNameExistInDatabase(string name)
        {
            var customer = new Customer
            {
                Name = name
            };
            mockRepository.Setup(m => m.GetByName(name)).Returns(customer);

            var result = service.GetByName(name);
            Assert.That(result, Is.SameAs(customer));
        }

        [TestCase("Adam")]
        [TestCase("Jozo")]
        [TestCase("Anastazia")]
        [TestCase("Eva")]
        public void CreateCustomer_ShouldReturnFalse_IfCustomerAlreadyExists(string name)
        {
            var existingCustomer = new Customer { Name=name };

            mockRepository.Setup(m => m.GetByName(name)).Returns(existingCustomer);

            var result = service.CreateAndSaveCustomer(name);

            Assert.That(result, Is.False);
        }

        [TestCase("Adam")]
        [TestCase("Jozo")]
        [TestCase("Anastazia")]
        [TestCase("Eva")]
        public void CreateCustomer_ShouldReturnTrue_IfCustomerWasCreated(string name)
        {
            mockRepository.Setup(m => m.GetByName(name)).Returns(() => null);
            mockRepository.Setup(m => m.AddEntity(It.Is<Customer>(c=>c.Name==name)));
            mockRepository.Setup(m => m.SaveChanges()).Returns(true);

            var result = service.CreateAndSaveCustomer(name);

            Assert.That(result, Is.True);
            mockRepository.Verify(m => m.AddEntity(It.Is<Customer>(c => c.Name == name)), Times.Once);
            mockRepository.Verify(m => m.SaveChanges(), Times.Once);
        }


        [Test]
        public void ReturnCar_ShouldReturnTrue_IfCarWasReturned()
        {
            var customer = new Customer();
            mockRepository.Setup(m => m.SaveChanges()).Returns(false);

            var result = service.ReturnCar(customer);

            Assert.That(result, Is.False);
        }
        [Test]
        public void ReturnCar_ShouldReturnFalse_IfCarWasNotReturnSuccessful()
        {
            var customer = new Customer();
            mockRepository.Setup(m => m.SaveChanges()).Returns(true);

            var result = service.ReturnCar(customer);

            Assert.That(result, Is.True);
        }

        [Test]
        public void RentCar_ShouldReturnTrue_IfReturnCarWasSuccess()
        {
            var customer = new Customer();
            var car = new Car();
            mockRepository.Setup(m => m.SaveChanges()).Returns(true);

            var result = service.RentCar(customer, car);

            Assert.That(result, Is.True);
        }

        [Test]
        public void RentCar_ShouldReturnFalse_IfReturnCarWasNotSuccess()
        {
            var customer = new Customer();
            var car = new Car();
            mockRepository.Setup(m => m.SaveChanges()).Returns(false);

            var result = service.RentCar(customer, car);

            Assert.That(result, Is.False);
        }

        [Test]
        public void AlreadyRentedCar_ShouldReturnNull_IfCustomerDoesNotHaveRentedCar()
        {
            var customer = new Customer()
            {
                Rented_Car_Id = 0
            };
            var result = service.AlreadyRentedCar(customer);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void AlreadyRentedCar_ShouldReturnCar_IfCustomerHaveRentCar()
        {
            var expectedCar = new Car()
            {
                Id = 5
            };
            var customer = new Customer()
            {
                Car = expectedCar
            };
            var result = service.AlreadyRentedCar(customer);

            Assert.That(result, Is.EqualTo(expectedCar));
        }


        //[Test] vzor
        public void SomeTest()
        {
            bool premenna = true;
            Assert.That(premenna, Is.True);  //ci je premenna true 
            Assert.That(premenna, Is.False);//ci je premenna false 
            Assert.That(premenna, Is.EqualTo(true));//ci sa premenna rovna true 
            Assert.That(premenna, Is.EqualTo(false));//ci sa premenna rovna true
            Assert.That(premenna, Is.Null); //ci je premenna null 
            Assert.That(premenna, Is.Not.Null); //ci je premenna not null 

            var collection1 = new List<int> { 1, 2, 3 };
            var collection2 = new List<int> { 1, 2, 3 };
            Assert.That(collection1, Is.EqualTo(collection2));//equal porovna referencne hodnoty,test fail
            CollectionAssert.AreEqual(collection1, collection2);//porovna hodnoty, ak su polia identicke , test prejde 
            CollectionAssert.AreEquivalent(collection1, collection2);//kontroluje jednotlive item ci su identicke,
                                                                     //ale nemusia byt v tom istom poradi 
            Assert.Throws<NullReferenceException>(() => premenna = false);//Overuje, že vykonanie kódu vyvolá špecifickú výnimku.
        }
    }
}
