using CRMApp_datalayer.IRepositories;
using CRMApp_datalayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.Repositories
{
    public class CompanyDB : ICompanyRepo
    {
        private CRMAppContext db = new CRMAppContext();
        public Company AddNewCompany(Company company)
        {
            db.Companies.Add(company);
            db.SaveChanges();
            return company;
        }

        public List<Company> GetAllCompany()
        {
            return db.Companies.ToList();
        }

        public Company GetCompany(int id)
        {            
            return db.Companies.Find(id);
        }

        public bool RemoveCompany(int id)
        {
            Company comp = GetCompany(id);
            if (comp == null)
            {
                return false;                
            }
            db.Companies.Remove(comp);
            db.SaveChanges();
            return true;
        }

        public Company UpdateCompany(int id, Company company)
        {
            Company uCompany = GetCompany(id);
            if (uCompany != null)
            {
                uCompany.Name = company.Name;
                uCompany.Address = company.Address;
                uCompany.PostAddress = company.PostAddress;
                uCompany.Telephone = company.Telephone;
                uCompany.Email = company.Email;
                uCompany.Updated = DateTime.Now;
                db.SaveChanges();
            }

            return uCompany;
        }     
        private bool CompanyExists(int id)
        {
            return db.Companies.Count(e => e.Id == id) > 0;
        }       
    }
}
