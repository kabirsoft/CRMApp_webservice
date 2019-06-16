using CRMApp_datalayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.IRepositories
{
    public interface IContactRepo
    {
        List<Contact> GetAllContacts();
        Contact AddNewContact(Contact contact);
        Contact GetContact(int id);
        Contact UpdateContact(int id, Contact contact);
        bool RemoveContact(int id);
        List<Contact> ContactsByCustomerId(int customerId);

    }
}
