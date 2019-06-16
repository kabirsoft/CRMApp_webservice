using CRMApp_datalayer.IRepositories;
using CRMApp_datalayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.Repositories
{
    public class CustomerTypeDB : ICustomerTypeRepo
    {
        private CRMAppContext db = new CRMAppContext();
        public CustomerType AddNewCustomerType(CustomerType custType)
        {
            db.CustomerTypes.Add(custType);
            db.SaveChanges();
            return custType;
        }

        public List<CustomerType> GetAllCustomerType()
        {
            return db.CustomerTypes.ToList();
        }

        public CustomerType GetCustomerType(int id)
        {
            return db.CustomerTypes.Find(id);
        }

        public bool RemoveCustomerType(int id)
        {
            CustomerType ctype = GetCustomerType(id);
            if(ctype == null)
            {
                return false;
            }
            db.CustomerTypes.Remove(ctype);
            db.SaveChanges();
            return true;
        }

        public CustomerType UpdateCustomerType(int id, CustomerType custType)
        {
            CustomerType uCtype = GetCustomerType(id);
            if(uCtype != null)
            {
                uCtype.Name = custType.Name;
                uCtype.Updated = DateTime.Now;
                db.SaveChanges();
            }
            return custType;
        }
    }
}
