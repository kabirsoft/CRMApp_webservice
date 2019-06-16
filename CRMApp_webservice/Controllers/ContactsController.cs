using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CRMApp_datalayer.IRepositories;
using CRMApp_datalayer.Models;

namespace CRMApp_webservice.Controllers
{
    public class ContactsController : ApiController
    {
        private IContactRepo ContactRepo;
        public ContactsController(IContactRepo _contactRepo)
        {
            this.ContactRepo = _contactRepo;
        }

        [HttpGet]
        [Route("api/contacts")]
        [ResponseType(typeof(List<Contact>))]
        public List<Contact> Get()
        {
            return ContactRepo.GetAllContacts();
        }
        [HttpGet]
        [Route("api/contacts/{id}")]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult Get(int id)
        {
            Contact contact = ContactRepo.GetContact(id);
            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }
        [HttpPost]
        [Route("api/contacts/")]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult Post(Contact contact)
        {
            Contact newContact = ContactRepo.AddNewContact(contact);
            if(newContact == null)
            {
                return NotFound();
            }
            return Ok(newContact);
        }

        [HttpPut]
        [Route("api/contacts/{id}")]
        [ResponseType(typeof(Contact))]
        public IHttpActionResult Put(int id, Contact contact)
        {
            Contact newContact = ContactRepo.UpdateContact(id, contact);
            if(newContact == null)
            {
                return NotFound();
            }
            return Ok(newContact);
        }

        [HttpDelete]
        [Route("api/contacts/{id}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(int id)
        {
            bool res = ContactRepo.RemoveContact(id);
            if (!res)
            {
                return NotFound();
            }
            return Ok(true);
        }

    }
}
