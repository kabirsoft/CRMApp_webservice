using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRMApp_datalayer.IRepositories;
using CRMApp_datalayer.Models;

namespace CRMApp_datalayer.Controllers
{
    public class ContactsController : Controller
    {
        private CRMAppContext db = new CRMAppContext();
        private IContactRepo ContactRepo;
        private ICustomerRepo CustomerRepo;
        public ContactsController(IContactRepo _contactRepo, ICustomerRepo _customerRepo)
        {
            this.ContactRepo = _contactRepo;
            this.CustomerRepo = _customerRepo;
        }

        // GET: Contacts
        public ActionResult Index()
        {
            //var contacts = db.Contacts.Include(c => c.Customer);
            //return View(contacts.ToList());
            return View(ContactRepo.GetAllContacts());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = ContactRepo.GetContact(Convert.ToInt32(id));
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstName,Title,Telephone,Mobile,Email,Created,Updated,CustomerId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                ContactRepo.AddNewContact(contact);
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", contact.CustomerId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = ContactRepo.GetContact(Convert.ToInt32(id));
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", contact.CustomerId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstName,Title,Telephone,Mobile,Email,Created,Updated,CustomerId")] Contact contact)
        {
            if (ModelState.IsValid)
            {               
                ContactRepo.UpdateContact(contact.Id, contact);
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", contact.CustomerId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = ContactRepo.GetContact(Convert.ToInt32(id));
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactRepo.RemoveContact(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ContactsByCustomerId(int id)
        {
            List<Contact> contacts = ContactRepo.ContactsByCustomerId(id);
            ViewBag.customerName = CustomerRepo.GetCustomer(id).Name;
            return View(contacts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
