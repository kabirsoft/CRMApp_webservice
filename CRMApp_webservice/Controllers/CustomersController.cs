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
    public class CustomersController : ApiController
    {
        private ICustomerRepo CustomerRepo;
        public CustomersController( ICustomerRepo _customerRepo)
        {
            this.CustomerRepo = _customerRepo;
        }

        [HttpGet]
        [Route("api/customers")]
        [ResponseType(typeof(List<Customer>))]
        public List<Customer> Get()
        {
            return CustomerRepo.GetAllCustomer();
        }
        [HttpGet]
        [Route("api/customers/{id}")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult Get(int id)
        {
            Customer customer = CustomerRepo.GetCustomer(id);
            if( customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        [Route("api/customers")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult Post(Customer customer)
        {
            Customer newCustomer = CustomerRepo.AddNewCustomer(customer);
            if(newCustomer == null)
            {
                return NotFound();
            }
            return Ok(newCustomer);
        }

        [HttpPut]
        [Route("api/customers/{id}")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult Put(int id, Customer customer)
        {
            Customer newCustomer = CustomerRepo.UpdateCustomer(id, customer);
            if(newCustomer == null)
            {
                return NotFound();
            }
            return Ok(newCustomer);
        }

        [HttpDelete]
        [Route("api/customers/{id}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(int id)
        {
            bool res = CustomerRepo.RemoveCustomer(id);
            if(!res)
            {
                return NotFound();
            }
            return Ok(true);
        }
    }
}
