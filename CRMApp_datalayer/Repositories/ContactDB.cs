using CRMApp_datalayer.IRepositories;
using CRMApp_datalayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_datalayer.Repositories
{
    public class ContactDB : IContactRepo
    {
        private CRMAppContext db = new CRMAppContext();
        public Contact AddNewContact(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
            return contact;
        }

        public List<Contact> GetAllContacts()
        {
            return db.Contacts.ToList();
        }

        public Contact GetContact(int id)
        {
            return db.Contacts.Find(id);
        }

        public bool RemoveContact(int id)
        {
            Contact cont = GetContact(id);
            if(cont == null)
            {
                return false;
            }
            db.Contacts.Remove(cont);
            db.SaveChanges();
            return true;
        }

        public Contact UpdateContact(int id, Contact contact)
        {
            Contact uContact = GetContact(id);
            if(uContact != null)
            {
                uContact.LastName = contact.LastName;
                uContact.FirstName = contact.FirstName;
                uContact.Title = contact.Title;
                uContact.Telephone = contact.Telephone;
                uContact.Mobile = contact.Mobile;
                uContact.Email = contact.Email;
                uContact.CustomerId = contact.CustomerId;
                uContact.Updated = DateTime.Now;                
                db.SaveChanges();
            }
            return uContact;
        }
        public List<Contact> ContactsByCustomerId(int id)
        {
            return db.Contacts.Where(x => x.CustomerId == id).ToList();
        }

    }
}
