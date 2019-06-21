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
    public class UnitTest_contacts
    {
        [TestMethod]
        public void TestContactsGet()
        {
            //arrange
            var ContactRepoMockClass = new Mock<IContactRepo>();
            List<Contact> getContactObj = new List<Contact>()
            {
                new Contact{Id=1, FirstName="John", LastName="Doe",  Title="Manager", CustomerId=1, Email="john@yahoo.com", Mobile="66554433", Telephone="77665544", Created=DateTime.Now, Updated=DateTime.Now  },
                new Contact{Id=2, FirstName="Lynda", LastName="pia",  Title="HR", CustomerId=1, Email="lynda@gmail.com", Mobile="22554444", Telephone="44665555", Created=DateTime.Now, Updated=DateTime.Now  }
            };
            ContactRepoMockClass.Setup(x => x.GetAllContacts()).Returns(getContactObj);
            var contactsController = new ContactsController(ContactRepoMockClass.Object);

            //Act
            List<Contact> result = contactsController.Get();

            //assert
            Assert.AreEqual(result[0].Id, 1);
            Assert.AreEqual(result[1].Email, "lynda@gmail.com");
        }
        [TestMethod]
        public void TestContactsGetContact()
        {
            //arrange
            var ContactRepoMockClass = new Mock<IContactRepo>();
            var getContactObj = new Contact { Id = 1, FirstName = "John", LastName = "Doe", Title = "Manager", CustomerId = 1, Email = "john@yahoo.com", Mobile = "66554433", Telephone = "77665544", Created = DateTime.Now, Updated = DateTime.Now };
            ContactRepoMockClass.Setup(x => x.GetContact(1)).Returns(getContactObj);
            var contactsController = new ContactsController(ContactRepoMockClass.Object);

            //act
            IHttpActionResult result = contactsController.Get(1);
            var resultContact = result as OkNegotiatedContentResult<Contact>;


            //assert
            Assert.AreEqual(resultContact.Content.Title, "Manager");
            Assert.AreEqual(resultContact.Content.Id, 1);
        }

        [TestMethod]
        public void TestContactsPost()
        {
            //arrange
            var ContactRepoMockClass = new Mock<IContactRepo>();

            var contact = new Contact { FirstName = "John", LastName = "Doe", Title = "Manager", CustomerId = 1, Email = "john@yahoo.com", Mobile = "66554433", Telephone = "77665544", Created = DateTime.Now, Updated = DateTime.Now };
            ContactRepoMockClass.Setup(x => x.AddNewContact(contact)).Returns(contact);
            var contactsController = new ContactsController(ContactRepoMockClass.Object);

            //act
            IHttpActionResult result = contactsController.Post(contact);
            var resultContact = result as OkNegotiatedContentResult<Contact>;

            //assert
            Assert.AreEqual(resultContact.Content.Title, "Manager");
            Assert.AreEqual(resultContact.Content.FirstName, "John");
        }

        [TestMethod]
        public void TestContactsPut()
        {
            //arrange
            var ContactsRepoMockClass = new Mock<IContactRepo>();
            var contact = new Contact { FirstName = "John", LastName = "Doe", Title = "Manager", CustomerId = 1, Email = "john@yahoo.com", Mobile = "66554433", Telephone = "77665544", Created = DateTime.Now, Updated = DateTime.Now };
            ContactsRepoMockClass.Setup(x => x.UpdateContact(1, contact)).Returns(contact);
            var ContactsController = new ContactsController(ContactsRepoMockClass.Object);

            //act
            IHttpActionResult result = ContactsController.Put(1, contact);
            var resultContact = result as OkNegotiatedContentResult<Contact>;

            //assert
            Assert.AreEqual(resultContact.Content, contact);
            Assert.AreEqual(resultContact.Content.Title, "Manager");
            Assert.AreEqual(resultContact.Content.FirstName, "John");
        }

        [TestMethod]
        public void TestContactsDelete()
        {
            //arrange
            var ContactsRepoMockClass = new Mock<IContactRepo>();
            var expected = true;
            ContactsRepoMockClass.Setup(x => x.RemoveContact(1)).Returns(expected);
            var contactsController = new ContactsController(ContactsRepoMockClass.Object);

            //act
            IHttpActionResult result = contactsController.Delete(1);
            var resultCompany = result as OkNegotiatedContentResult<bool>;

            //assert
            Assert.AreEqual(resultCompany.Content, true);

        }

        [TestMethod]
        public void TestContactsByCustomerId()
        {
            //arrange
            var ContactRepoMockClass = new Mock<IContactRepo>();
            List<Contact> getContactObj = new List<Contact>()
            {
                new Contact{Id=1, FirstName="John", LastName="Doe",  Title="Manager", CustomerId=1, Email="john@yahoo.com", Mobile="66554433", Telephone="77665544", Created=DateTime.Now, Updated=DateTime.Now  },
                new Contact{Id=2, FirstName="Lynda", LastName="pia",  Title="HR", CustomerId=1, Email="lynda@gmail.com", Mobile="22554444", Telephone="44665555", Created=DateTime.Now, Updated=DateTime.Now  }
            };
            ContactRepoMockClass.Setup(x => x.ContactsByCustomerId(1)).Returns(getContactObj);
            var contactsController = new ContactsController(ContactRepoMockClass.Object);

            //Act
            IHttpActionResult result = contactsController.ContactsByCustomerId(1);
            var resultContact = result as OkNegotiatedContentResult<List<Contact>>;

            //assert
            Assert.AreEqual(resultContact.Content[0].Title, "Manager");
            Assert.AreEqual(resultContact.Content[1].FirstName, "Lynda");

        }
    }
}
