using CRMApp_datalayer.IRepositories;
using CRMApp_datalayer.Models;
using CRMApp_datalayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.Repositories
{
    public class CustomerDB : ICustomerRepo
    {
        private CRMAppContext db = new CRMAppContext();
        public Customer AddNewCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            return customer;
        }

        public List<Customer> GetAllCustomer()
        {
            return db.Customers.ToList();
        }

        public Customer GetCustomer(int id)
        {
            return db.Customers.Find(id);
        }

        public bool RemoveCustomer(int id)
        {
            Customer cust = GetCustomer(id);
            if(cust == null)
            {
                return false;
            }
            db.Customers.Remove(cust);
            db.SaveChanges();
            return true;
        }

        public Customer UpdateCustomer(int id, Customer customer)
        {
            Customer uCustomer = GetCustomer(id);
            if (uCustomer != null)
            {
                uCustomer.Name = customer.Name;
                uCustomer.Address = customer.Address;
                uCustomer.PostAddress = customer.PostAddress;
                uCustomer.Telephone = customer.Telephone;
                uCustomer.Fax = customer.Fax;
                uCustomer.CompanyId = customer.CompanyId;
                uCustomer.Updated = DateTime.Now;
                db.SaveChanges();
            }

            return uCustomer;
        }

        public Customer UpdateCustomerWithTypes(CustomerViewModel customer)
        {
            var uCustomer = GetCustomer(customer.Id);
            if (uCustomer != null)
            {                
                uCustomer.Name = customer.Name;
                uCustomer.Address = customer.Address;
                uCustomer.PostAddress = customer.Address;
                uCustomer.Telephone = customer.Telephone;
                uCustomer.Fax = customer.Fax;
                uCustomer.CompanyId = customer.CompanyId;
                uCustomer.Updated = DateTime.Now;

                foreach (var item in db.Customer_CustomerTypes)
                {
                    if (item.CustomerId == customer.Id)
                    {
                        db.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                    }
                }
                foreach (var item in customer.TypeVmList)
                {
                    if (item.Checked)
                    {
                        db.Customer_CustomerTypes.Add(new Customer_CustomerType { CustomerId = customer.Id, CustomerTypeId = item.TypeID });
                    }
                }
                db.SaveChanges();
            }

            return uCustomer;
        }

        public CustomerViewModel GetCustomerWithTypesByCustomerId(int id)
        {
            Customer customer = GetCustomer(id);
            if (customer == null)
            {
                return null;
            }

            var CustomerWithTypes = from ct in db.CustomerTypes
                                    select new
                                    {
                                        ct.Id,
                                        ct.Name,
                                        Checked = ((from cct in db.Customer_CustomerTypes
                                                    where (cct.CustomerId == id) & (cct.CustomerTypeId == ct.Id)
                                                    select cct).Count() > 0)
                                    };

            CustomerViewModel CustomerVm = new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                PostAddress = customer.PostAddress,
                Telephone = customer.Telephone,
                Fax = customer.Fax,
                CompanyId = customer.CompanyId
            };

            List<TypeViewModel> typeList = new List<TypeViewModel>();
            foreach (var t in CustomerWithTypes)
            {
                typeList.Add(new TypeViewModel { TypeID = t.Id, Name = t.Name, Checked = t.Checked });
            }
            CustomerVm.TypeVmList = typeList;
            return CustomerVm;
        }

        public List<Customer> GetCustomerByType(int TypeId)
        {
            var customerList =
           from cust in db.Customers
           join cct in db.Customer_CustomerTypes on cust.Id equals cct.CustomerId into custGroup
           from type in custGroup
           where type.CustomerTypeId == TypeId
           select cust;

            return customerList.ToList();
        }
    }
}
