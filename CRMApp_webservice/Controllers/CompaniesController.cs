using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CRMApp_datalayer.IRepositories;
using CRMApp_datalayer.Models;

namespace CRMApp_webservice.Controllers
{
    public class CompaniesController : ApiController
    {
        private ICompanyRepo CompanyRepo; 
        public CompaniesController(ICompanyRepo _companyRepo )
        {
            this.CompanyRepo = _companyRepo;
        }

        // GET: api/Companies
        [HttpGet]
        [Route("api/companies")]
        //[ResponseType(typeof(List<Company>))]
        public List<Company> Get()
        {           
            return CompanyRepo.GetAllCompany();
        }

        [HttpGet]
        [ResponseType(typeof(Company))]       
        [Route("api/companies/{id}")]
        public IHttpActionResult GetCompany(int id)
        {
            Company company = CompanyRepo.GetCompany(id);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(company);
        }

        
        [HttpPut]
        [Route("api/companies/{id}")]
        [ResponseType(typeof(Company))]
        public IHttpActionResult Put(int id, Company company)
        {
            Company UpdatedCompany = CompanyRepo.UpdateCompany(id, company);
            if(UpdatedCompany == null)
            {
                return NotFound();
            }
            return Ok(UpdatedCompany);
        }

        [HttpPost]
        [Route("api/companies/")]
        [ResponseType(typeof(Company))]
        public IHttpActionResult Post(Company company)
        {
            Company newCompany = CompanyRepo.AddNewCompany(company);
            if(newCompany == null)
            {
                return NotFound();
            }
            return Ok(newCompany);
        }

        [HttpDelete]
        [ResponseType(typeof(bool))]
        [Route("api/companies/{id}")]
        public IHttpActionResult Delete(int id)
        {
            bool company = CompanyRepo.RemoveCompany(id);
            if (company == false)
            {
                return NotFound();
            }
            return Ok(true);
        }       
    }
}