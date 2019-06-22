using CRMApp_datalayer.Models;
using CRMApp_datalayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.IRepositories
{
    public interface ICustomerRepo
    {
        List<Customer> GetAllCustomer();
        Customer GetCustomer(int id);
        Customer AddNewCustomer(Customer customer);
        //Customer UpdateCustomer(int id, Customer customer);
        bool RemoveCustomer(int id);
        CustomerViewModel GetCustomerWithTypesByCustomerId(int id);
        Customer UpdateCustomerWithTypes(CustomerViewModel customer);
        List<Customer> GetCustomerByType(int TypeId);
        List<Customer> GetCustoerByTxtBgn(string searchTxt);
    }
}
