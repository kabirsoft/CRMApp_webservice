using CRMApp_datalayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.IRepositories
{
    public interface ICustomerTypeRepo
    {
        List<CustomerType> GetAllCustomerType();
        CustomerType AddNewCustomerType(CustomerType custType);
        CustomerType GetCustomerType(int id);
        CustomerType UpdateCustomerType(int id, CustomerType custType);
        bool RemoveCustomerType(int id);
    }
}
