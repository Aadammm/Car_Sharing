using Car_Sharing.Data;
using Car_Sharing.DataAccess.Interface;
using Car_Sharing.Models;
using Car_Sharing.Services;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Sharing_Tests
{
    public class CompanyServiceTests
    {

        Mock<ICompanyRepository> mockRepository;
        CompanyService service;
        [SetUp]
        public void SetUp()
        {
            mockRepository = new Mock<ICompanyRepository>();//musi sa vytvorit mock na fake pracu s repository(praca s databazou)
            service = new CompanyService(mockRepository.Object);//tato trieda sa testuje tak nemoze byt mockovana, ale jej zavislosti musia byt 
                                                                 //CustomerService ma v konstruktore ako parameter CustomerRepository kvoli Dependency Inversion 
        }

        [TestCase("Company")]
        [TestCase("CoCaCola")]
        [TestCase("Rent4You")]
        [TestCase("AllCars")]
        public void GetByName_ShouldReturnCompany_IfCompanyWithThisNameIsExistInDatabase(string name)
        {
            var company = new Company()
            {
                Name=name
            };
            mockRepository.Setup(c => c.GetByName(name)).Returns(company);

            var result = service.GetByName(name);

            Assert.That(result, Is.SameAs(company));
        }

        [TestCase("Company")]
        [TestCase("CoCaCola")]
        [TestCase("Rent4You")]
        [TestCase("AllCars")]
        public void GetByName_ShouldReturnNull_IfCompanyWithThisNameIsNotExistInDatabase(string name)
        {
            Company company = null;
            mockRepository.Setup(c => c.GetByName(name)).Returns((Company)null);

            var result = service.GetByName(name);

            Assert.That(result, Is.SameAs(company));
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
        public void GetCompanies_ShouldReturnIEnumerableOfCompanies_IfExistsInDatabase()
        {
            //arrange
            var companies = new List<Company>() {
            new Company()
            {
                Id = 1,
                Name = "Adam"
            },new Company()
            {
                Id = 12,
                Name = ""
            }   };
            mockRepository.Setup(m => m.GetAll()).Returns(companies);

            // Act
            var result = service.GetCompanies();
            // Assert

            //Assert.That(result.Count(), Is.EqualTo(abc.Count));
            CollectionAssert.AreEquivalent(result, companies);
        }
        [Test]
        public void GetCompanies_ShouldReturnEmptyEnumerable_IfNotExistCompaniesInDatabase()
        { 
            mockRepository.Setup(m => m.GetAll()).Returns(Enumerable.Empty<Company>());

            var result = service.GetCompanies();

            Assert.That(result, Is.Empty);
        }

        [TestCase("Company")]
        [TestCase("CoCaCola")]
        [TestCase("Rent4You")]
        [TestCase("AllCars")]
        public void CreateCompany_ShouldReturnTrue_IfCompanyWasCreatedSuccessful(string name)
        {
            mockRepository.Setup(m=>m.GetByName(name)).Returns((Company)null);
            mockRepository.Setup(m => m.AddEntity(It.Is<Company>(c => c.Name == name)));
            mockRepository.Setup(m => m.SaveChanges()).Returns(true);

            var result = service.CreateAndSaveCompany(name);

            Assert.That(result, Is.True);
        }

        [TestCase("Company")]
        [TestCase("CoCaCola")]
        [TestCase("Rent4You")]
        [TestCase("AllCars")]
        public void CreateCompany_ShouldReturnFalse_IfCompanyAlreadyExists(string name)
        {
            var existingCompany = new Company { Name = name };

            mockRepository.Setup(m => m.GetByName(name)).Returns(existingCompany);

            var result = service.CreateAndSaveCompany(name);

            Assert.That(result, Is.False);
        }


    }
}
