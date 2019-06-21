using CRMApp_datalayer.IRepositories;
using CRMApp_datalayer.Models;
using CRMApp_datalayer.ViewModels;
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
    public class UnitTest_customers
    {
        [TestMethod]
        public void TestCustomersGet()
        {
            //arrange
            var CustomerRepoMockClass = new Mock<ICustomerRepo>();
            List<Customer> getCustomerObj = new List<Customer>()
            {
                new Customer{Id=1, Name="Cust1", Address="Oslo", PostAddress="1024", Telephone="123341", Fax="134451", CompanyId=1, Created=DateTime.Now, Updated=DateTime.Now},
                new Customer{Id=2, Name="Cust2", Address="Bergen", PostAddress="2024", Telephone="223342", Fax="234452", CompanyId=2, Created=DateTime.Now, Updated=DateTime.Now}
            };
            CustomerRepoMockClass.Setup(x => x.GetAllCustomer()).Returns(getCustomerObj);
            var customerController = new CustomersController(CustomerRepoMockClass.Object);

            //Act
            List<Customer> result = customerController.Get();

            //assert
            Assert.AreEqual(result[0].Id, 1);
            Assert.AreEqual(result[1].Name, "Cust2");
        }

        [TestMethod]
        public void TestCustomersGetCustomer()
        {
            //arrange
            var CustomerRepoMockClass = new Mock<ICustomerRepo>();
            var getCustomerObj = new Customer { Id = 1, Name = "Cust1", Address = "Oslo", PostAddress = "1024", Telephone = "123341", Fax = "134451", CompanyId = 1, Created = DateTime.Now, Updated = DateTime.Now };
            CustomerRepoMockClass.Setup(x => x.GetCustomer(1)).Returns(getCustomerObj);
            var customerController = new CustomersController(CustomerRepoMockClass.Object);

            //act
            IHttpActionResult result = customerController.Get(1);
            var resultCustomer = result as OkNegotiatedContentResult<Customer>;

            //assert
            Assert.AreEqual(resultCustomer.Content.Id, 1);
            Assert.AreEqual(resultCustomer.Content.Name, "Cust1");
        }

        [TestMethod]
        public void TestCustomersPost()
        {
            //arrange
            var CustomerRepoMockClass = new Mock<ICustomerRepo>();

            var customer = new Customer { Name = "Cust1", Address = "Oslo", PostAddress = "1024", Telephone = "123341", Fax = "134451", CompanyId = 1, Created = DateTime.Now, Updated = DateTime.Now };
            CustomerRepoMockClass.Setup(x => x.AddNewCustomer(customer)).Returns(customer);
            var customersController = new CustomersController(CustomerRepoMockClass.Object);

            //act
            IHttpActionResult result = customersController.Post(customer);
            var resultCustomer = result as OkNegotiatedContentResult<Customer>;

            //assert
            Assert.AreEqual(resultCustomer.Content.Address, "Oslo");
            Assert.AreEqual(resultCustomer.Content.Name, "Cust1");
        }

        [TestMethod]
        public void TestCustomersPut()
        {
            //arrange
            var CustomersRepoMockClass = new Mock<ICustomerRepo>();
            var customerVm = new CustomerViewModel{Id=1, Name = "Cust1", Address = "Oslo", PostAddress = "1024", Telephone = "123341", Fax = "134451", CompanyId = 1,  Updated = DateTime.Now, TypeVmList=null };
            var customer = new Customer { Id = 1, Name = "Cust1", Address = "Oslo", PostAddress = "1024", Telephone = "123341", Fax = "134451", CompanyId = 1, Updated = DateTime.Now};

            CustomersRepoMockClass.Setup(x => x.UpdateCustomerWithTypes(customerVm)).Returns(customer);
            var customersController = new CustomersController(CustomersRepoMockClass.Object);

            //act
            IHttpActionResult result = customersController.Put(customerVm);
            var resultCustomer = result as OkNegotiatedContentResult<Customer>;

            //assert
            Assert.AreEqual(resultCustomer.Content, customer);
            Assert.AreEqual(resultCustomer.Content.Id, 1);
            Assert.AreEqual(resultCustomer.Content.Name, "Cust1");
        }

        [TestMethod]
        public void TestCustomersDelete()
        {
            //arrange
            var CustomersRepoMockClass = new Mock<ICustomerRepo>();
            var expected = true;
            CustomersRepoMockClass.Setup(x => x.RemoveCustomer(1)).Returns(expected);
            var customersController = new CustomersController(CustomersRepoMockClass.Object);

            //act
            IHttpActionResult result = customersController.Delete(1);
            var resultCustomer = result as OkNegotiatedContentResult<bool>;

            //assert
            Assert.AreEqual(resultCustomer.Content, true);

        }

    }
}
