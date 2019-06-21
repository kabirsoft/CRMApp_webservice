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
    public class UnitTest_customerTypes
    {
        [TestMethod]
        public void TestGetAllCustomerType()
        {
            //arrange
            var CustomerTypeRepoMockClass = new Mock<ICustomerTypeRepo>();
            List<CustomerType> getTypeObj = new List<CustomerType>()
            {
                new CustomerType{Id=1, Name="Bank", Created=DateTime.Now, Updated=DateTime.Now},
                new CustomerType{Id=2, Name="Kommune", Created=DateTime.Now, Updated=DateTime.Now},
                new CustomerType{Id=3, Name="Flyselskap", Created=DateTime.Now, Updated=DateTime.Now}

            };
            CustomerTypeRepoMockClass.Setup(x => x.GetAllCustomerType()).Returns(getTypeObj);
            var typesController = new CustomerTypesController(CustomerTypeRepoMockClass.Object);

            //Act
            List<CustomerType> result = typesController.Get();

            //assert
            Assert.AreEqual(result[0].Id, 1);
            Assert.AreEqual(result[1].Name, "Kommune");
        }

        [TestMethod]
        public void TestGetCustomerType()
        {
            //arrange
            var CustomerTypeRepoMockClass = new Mock<ICustomerTypeRepo>();
            var getTypeObj = new CustomerType { Id = 1, Name = "Bank", Created = DateTime.Now, Updated = DateTime.Now };
            CustomerTypeRepoMockClass.Setup(x => x.GetCustomerType(1)).Returns(getTypeObj);
            var typesController = new CustomerTypesController(CustomerTypeRepoMockClass.Object);

            //act
            IHttpActionResult result = typesController.Get(1);
            var resultType = result as OkNegotiatedContentResult<CustomerType>;


            //assert
            Assert.AreEqual(resultType.Content.Id, 1);
            Assert.AreEqual(resultType.Content.Name, "Bank");
        }

        [TestMethod]
        public void TestTypesPost()
        {
            //arrange
            var TypesRepoMockClass = new Mock<ICustomerTypeRepo>();

            var types = new CustomerType { Name = "Bank", Created = DateTime.Now, Updated = DateTime.Now };
            TypesRepoMockClass.Setup(x => x.AddNewCustomerType(types)).Returns(types);
            var typesController = new CustomerTypesController(TypesRepoMockClass.Object);

            //act
            IHttpActionResult result = typesController.Post(types);
            var resultType = result as OkNegotiatedContentResult<CustomerType>;

            //assert
            Assert.AreEqual(resultType.Content.Name, "Bank");
        }

        [TestMethod]
        public void TestTypesPut()
        {
            //arrange
            var TypesRepoMockClass = new Mock<ICustomerTypeRepo>();
            var type = new CustomerType { Name = "Bank", Created = DateTime.Now, Updated = DateTime.Now };
            TypesRepoMockClass.Setup(x => x.UpdateCustomerType(1, type)).Returns(type);
            var TypesController = new CustomerTypesController(TypesRepoMockClass.Object);

            //act
            IHttpActionResult result = TypesController.Put(1, type);
            var resultType = result as OkNegotiatedContentResult<CustomerType>;

            //assert
            Assert.AreEqual(resultType.Content, type);
            Assert.AreEqual(resultType.Content.Name, "Bank");            
        }

        [TestMethod]
        public void TestTypesDelete()
        {
            //arrange
            var TypesRepoMockClass = new Mock<ICustomerTypeRepo>();
            var expected = true;
            TypesRepoMockClass.Setup(x => x.RemoveCustomerType(1)).Returns(expected);
            var typesController = new CustomerTypesController(TypesRepoMockClass.Object);

            //act
            IHttpActionResult result = typesController.Delete(1);
            var resultType = result as OkNegotiatedContentResult<bool>;

            //assert
            Assert.AreEqual(resultType.Content, true);

        }


    }
}
