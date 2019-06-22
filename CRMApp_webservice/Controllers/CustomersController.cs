using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CRMApp_datalayer.IRepositories;
using CRMApp_datalayer.Models;
using CRMApp_datalayer.ViewModels;

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
        [Route("api/customerWithTypes/{id}")]
        [ResponseType(typeof(CustomerViewModel))]
        public IHttpActionResult GetCustomerWithTypesByCustomerId(int? id)
        {
            var customer = CustomerRepo.GetCustomerWithTypesByCustomerId(Convert.ToInt32(id));
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet]
        [Route("api/customers/{id}")]
        [ResponseType(typeof(Customer))]
        public IHttpActionResult Get(int id)
        {
            Customer customer = CustomerRepo.GetCustomer(id);
            if (customer == null)
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
        [ResponseType(typeof(CustomerViewModel))]
        public IHttpActionResult Put(CustomerViewModel customer)
        {
            Customer UpdatedCustomer = CustomerRepo.UpdateCustomerWithTypes(customer);
            if (UpdatedCustomer == null)
            {
                return NotFound();
            }
            return Ok(UpdatedCustomer);
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

     

        [HttpPost]
        [Route("api/customers/GetCustomerByType")] 
        //http://localhost:50125/api/customers/GetCustomerByType?TypeId=2 //postman
        [ResponseType(typeof(List<Customer>))]
        public IHttpActionResult GetCustomerByType(int? TypeId)
        {
            if (TypeId == null)
            {
                return Ok(this.Get());
            }
            var customerList = CustomerRepo.GetCustomerByType(Convert.ToInt32(TypeId));
            return Ok(customerList);
        }

        [HttpPost]
        [Route("api/customers/GetCustomerByTxtBgn")]
        //http://localhost:50125/api/customers/GetCustomerByTxtBgn?searchTxt=cust3 //postman
        [ResponseType(typeof(List<Customer>))]
        public IHttpActionResult GetCustomerByTxtBgn(string searchTxt)
        {
            var customers = CustomerRepo.GetCustoerByTxtBgn(searchTxt);
            if(customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }
    }
}
