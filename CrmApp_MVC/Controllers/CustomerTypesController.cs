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
    public class CustomerTypesController : Controller
    {
        private CRMAppContext db = new CRMAppContext();
        private ICustomerTypeRepo CustomerTypeRepo;
        public CustomerTypesController(ICustomerTypeRepo _customerTypeRepo)
        {
            this.CustomerTypeRepo = _customerTypeRepo;
        }

        // GET: CustomerTypes
        public ActionResult Index()
        {
            return View(CustomerTypeRepo.GetAllCustomerType());
        }

        // GET: CustomerTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerType customerType = CustomerTypeRepo.GetCustomerType(Convert.ToInt32(id));
            if (customerType == null)
            {
                return HttpNotFound();
            }
            return View(customerType);
        }

        // GET: CustomerTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Created,Updated")] CustomerType customerType)
        {
            if (ModelState.IsValid)
            {
                CustomerTypeRepo.AddNewCustomerType(customerType);
                return RedirectToAction("Index");
            }

            return View(customerType);
        }

        // GET: CustomerTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerType customerType = CustomerTypeRepo.GetCustomerType(Convert.ToInt32(id));
            if (customerType == null)
            {
                return HttpNotFound();
            }
            return View(customerType);
        }

        // POST: CustomerTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Created,Updated")] CustomerType customerType)
        {
            if (ModelState.IsValid)
            {
                CustomerTypeRepo.UpdateCustomerType(customerType.Id, customerType);
                return RedirectToAction("Index");
            }
            return View(customerType);
        }

        // GET: CustomerTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerType customerType = CustomerTypeRepo.GetCustomerType(Convert.ToInt32(id));
            if (customerType == null)
            {
                return HttpNotFound();
            }
            return View(customerType);
        }

        // POST: CustomerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerTypeRepo.RemoveCustomerType(id);
            return RedirectToAction("Index");
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
