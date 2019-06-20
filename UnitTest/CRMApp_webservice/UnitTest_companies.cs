using CRMApp_datalayer.IRepositories;
using CRMApp_datalayer.Models;
using CRMApp_webservice.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace UnitTest.CRMApp_webservice
{
    [TestClass]
    public class UnitTest_companies
    {
        [TestMethod]
        public void TestCompaniesGet()
        {
            //arrange
            var CompanyRepoMockClass = new Mock<ICompanyRepo>();
            List<Company> getCompanyObj = new List<Company>()
            {
                new Company{Id=1, Name="Company1", Address="Oslo", PostAddress="1024", Email="test1@yahoo.com", Telephone="1334441", Created=DateTime.Now, Updated=DateTime.Now },
                new Company{Id=2, Name="Company2", Address="Bergen", PostAddress="2024", Email="test2@yahoo.com", Telephone="2334442", Created=DateTime.Now, Updated=DateTime.Now }
            };
            CompanyRepoMockClass.Setup(x => x.GetAllCompany()).Returns(getCompanyObj);
            var companiesController = new CompaniesController(CompanyRepoMockClass.Object);

            //Act
            List<Company> result = companiesController.Get();

            //assert
            Assert.AreEqual(result[0].Name, "Company1");
            Assert.AreEqual(result[1].Email, "test2@yahoo.com");
        }
        [TestMethod]
        public void TestCompaniesGetCompany()
        {
            //arrange
            var CompanyRepoMockClass = new Mock<ICompanyRepo>();
            var getCompanyObj = new Company { Id = 1, Name = "Company1", Address = "Oslo", PostAddress = "1024", Email = "test1@yahoo.com", Telephone = "1334441", Created = DateTime.Now, Updated = DateTime.Now };
            CompanyRepoMockClass.Setup(x => x.GetCompany(1)).Returns(getCompanyObj);
            var companiesController = new CompaniesController(CompanyRepoMockClass.Object);

            //act
            IHttpActionResult result = companiesController.GetCompany(1);
            var resultCompany = result as OkNegotiatedContentResult<Company>;


            //assert
            Assert.AreEqual(resultCompany.Content.Name, "Company1");
            Assert.AreEqual(resultCompany.Content.Id, 1);

        }
        [TestMethod]
        public void TestCompaniesPost()
        {
            //arrange
            var CompanyRepoMockClass = new Mock<ICompanyRepo>();

            var company = new Company { Name = "Company1", Address = "Oslo", PostAddress = "1024", Email = "test1@yahoo.com", Telephone = "1334441", Created = DateTime.Now, Updated = DateTime.Now };
            CompanyRepoMockClass.Setup(x => x.AddNewCompany(company)).Returns(company);
            var companiesController = new CompaniesController(CompanyRepoMockClass.Object);

            //act
            IHttpActionResult result = companiesController.Post(company);
            var resultCompany = result as OkNegotiatedContentResult<Company>;

            //assert
            Assert.AreEqual(resultCompany.Content.Name, "Company1");
            Assert.AreEqual(resultCompany.Content.Email, "test1@yahoo.com");
        }
        [TestMethod]
        public void TestCompaniesPut()
        {
            //arrange
            var CompanyRepoMockClass = new Mock<ICompanyRepo>();
            var company = new Company { Name = "Company1", Address = "Oslo", PostAddress = "1024", Email = "test1@yahoo.com", Telephone = "1334441", Created = DateTime.Now, Updated = DateTime.Now };
            CompanyRepoMockClass.Setup(x => x.UpdateCompany(1,company)).Returns(company);
            var companiesController = new CompaniesController(CompanyRepoMockClass.Object);

            //act
            IHttpActionResult result = companiesController.Put(1,company);
            var resultCompany = result as OkNegotiatedContentResult<Company>;

            //assert
            Assert.AreEqual(resultCompany.Content, company);
            Assert.AreEqual(resultCompany.Content.Name, "Company1");
            Assert.AreEqual(resultCompany.Content.Email, "test1@yahoo.com");
        }
        [TestMethod]
        public void TestCompaniesDelete()
        {
            //arrange
            var CompanyRepoMockClass = new Mock<ICompanyRepo>();
            var expected = true;
            CompanyRepoMockClass.Setup(x => x.RemoveCompany(1)).Returns(expected);
            var companiesController = new CompaniesController(CompanyRepoMockClass.Object);

            //act
            IHttpActionResult result = companiesController.Delete(1);
            var resultCompany = result as OkNegotiatedContentResult<bool>;

            //assert
            Assert.AreEqual(resultCompany.Content, true);

        }
    }
}
