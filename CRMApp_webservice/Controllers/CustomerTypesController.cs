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
    public class CustomerTypesController : ApiController
    {
        private ICustomerTypeRepo CustomerTypeRepo;
        public CustomerTypesController(ICustomerTypeRepo _customerTypeRepo)
        {
            this.CustomerTypeRepo = _customerTypeRepo;
        }

        [HttpGet]
        [Route("api/customertypes")]
        [ResponseType(typeof(List<CustomerType>))]
        public List<CustomerType> Get()
        {
            return CustomerTypeRepo.GetAllCustomerType();
        }

        [HttpGet]
        [Route("api/customertypes/{id}")]
        [ResponseType(typeof(CustomerType))]
        public IHttpActionResult Get(int id)
        {
            CustomerType custType = CustomerTypeRepo.GetCustomerType(id);
            if(custType == null)
            {
                return NotFound();
            }
            return Ok(custType);
        }
        [HttpPost]
        [Route("api/customertypes")]
        [ResponseType(typeof(CustomerType))]
        public IHttpActionResult Post(CustomerType custType)
        {
            CustomerType newCustType = CustomerTypeRepo.AddNewCustomerType(custType);
            if(newCustType == null)
            {
                return NotFound();
            }
            return Ok(newCustType);
        }
        [HttpPut]
        [Route("api/customertypes/{id}")]
        [ResponseType(typeof(CustomerType))]
        public IHttpActionResult Put(int id, CustomerType custType)
        {
            CustomerType newCustType = CustomerTypeRepo.UpdateCustomerType(id, custType);
            if(newCustType == null)
            {
                return NotFound();
            }
            return Ok(newCustType);
        }
        [HttpDelete]
        [Route("api/customertypes/{id}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(int id)
        {
            bool res = CustomerTypeRepo.RemoveCustomerType(id);
            if (!res)
            {
                return NotFound();
            }
            return Ok(true);
        }
    }
}
