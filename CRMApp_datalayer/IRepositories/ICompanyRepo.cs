using CRMApp_datalayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.IRepositories
{
    public interface ICompanyRepo
    {
        List<Company> GetAllCompany();
        Company GetCompany(int id);
        Company AddNewCompany(Company company);
        Company UpdateCompany(int id, Company company);
        bool RemoveCompany(int id);
    }
}
