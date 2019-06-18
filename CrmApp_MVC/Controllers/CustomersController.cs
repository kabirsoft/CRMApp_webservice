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
using CRMApp_datalayer.ViewModels;

namespace CRMApp_datalayer.Controllers
{
    public class CustomersController : Controller
    {
        private CRMAppContext db = new CRMAppContext();
        private ICustomerRepo CustomerRepo;
        public CustomersController(ICustomerRepo _customerRepo)
        {
            this.CustomerRepo = _customerRepo;
        }

        // GET: Customers
        public ActionResult Index()
        {
            ViewBag.TypeId = new SelectList(db.CustomerTypes, "Id", "Name");
            return View(CustomerRepo.GetAllCustomer());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }         
            
            Customer customer = CustomerRepo.GetCustomer(Convert.ToInt32(id));            
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,PostAddress,Telephone,Fax,Created,Updated,CompanyId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                CustomerRepo.AddNewCustomer(customer);
                return RedirectToAction("Index");
            }            
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", customer.CompanyId);           
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = CustomerRepo.GetCustomer(Convert.ToInt32(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            CustomerViewModel CustomerVm = CustomerRepo.GetCustomerWithTypesByCustomerId(Convert.ToInt32(id));
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", customer.CompanyId);
            return View(CustomerVm);
        }
        //public ActionResult Edit(int id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = CustomerRepo.GetCustomer(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", customer.CompanyId);
        //    return View(customer);
        //}

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                CustomerRepo.UpdateCustomerWithTypes(customer);
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", customer.CompanyId);
            return View(customer);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Address,PostAddress,Telephone,Fax,Created,Updated,CompanyId")] Customer customer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        CustomerRepo.UpdateCustomer(customer.Id, customer);             
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CompanyId = new SelectList(db.Companies, "Id", "Name", customer.CompanyId);
        //    return View(customer);
        //}

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = CustomerRepo.GetCustomer(Convert.ToInt32(id));
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {           
            CustomerRepo.RemoveCustomer(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult GetCustomerByType(int? TypeId)
        {
            ViewBag.TypeId = new SelectList(db.CustomerTypes, "Id", "Name");
            if (TypeId == null)
            {                
                return View("index",CustomerRepo.GetAllCustomer());
            }

            var customerList = CustomerRepo.GetCustomerByType(Convert.ToInt32(TypeId));
            return View("index", customerList);
        }
        [HttpPost]
        public ActionResult GetCustomerByTxtBgn(string searchTxt)
        {
            var customers = CustomerRepo.GetCustoerByTxtBgn(searchTxt);
            ViewBag.TypeId = new SelectList(db.CustomerTypes, "Id", "Name");
            return View("Index", customers);
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
