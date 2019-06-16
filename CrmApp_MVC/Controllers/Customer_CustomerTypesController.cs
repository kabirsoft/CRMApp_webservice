using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRMApp_datalayer.Models;

namespace CRMApp_datalayer.Controllers
{
    public class Customer_CustomerTypesController : Controller
    {
        private CRMAppContext db = new CRMAppContext();

        // GET: Customer_CustomerType
        public ActionResult Index()
        {
            var customer_CustomerTypes = db.Customer_CustomerTypes.Include(c => c.Customer).Include(c => c.CustomerType);
            return View(customer_CustomerTypes.ToList());
        }

        // GET: Customer_CustomerType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_CustomerType customer_CustomerType = db.Customer_CustomerTypes.Find(id);
            if (customer_CustomerType == null)
            {
                return HttpNotFound();
            }
            return View(customer_CustomerType);
        }

        //Assign types for customer
        [HttpGet]
        public ActionResult AssignTypesToCustomer()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.CustomerTypes = db.CustomerTypes;
            return View();
        }
  
        [HttpPost]       
        public ActionResult AssignTypesToCustomer(int customerId, int []typeids)
        {
            foreach(int tid in typeids)
            {
                Customer_CustomerType obj = new Customer_CustomerType();
                obj.CustomerId = customerId;
                obj.CustomerTypeId = tid;
                db.Customer_CustomerTypes.Add(obj);
                db.SaveChanges();
            }
            return RedirectToAction("index");
        }

        // GET: Customer_CustomerType/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.CustomerTypeId = new SelectList(db.CustomerTypes, "Id", "Name");
            return View();
        }

        // POST: Customer_CustomerType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,CustomerTypeId")] Customer_CustomerType customer_CustomerType)
        {
            if (ModelState.IsValid)
            {
                db.Customer_CustomerTypes.Add(customer_CustomerType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", customer_CustomerType.CustomerId);
            ViewBag.CustomerTypeId = new SelectList(db.CustomerTypes, "Id", "Name", customer_CustomerType.CustomerTypeId);
            return View(customer_CustomerType);
        }

        // GET: Customer_CustomerType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_CustomerType customer_CustomerType = db.Customer_CustomerTypes.Find(id);
            if (customer_CustomerType == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", customer_CustomerType.CustomerId);
            ViewBag.CustomerTypeId = new SelectList(db.CustomerTypes, "Id", "Name", customer_CustomerType.CustomerTypeId);
            return View(customer_CustomerType);
        }

        // POST: Customer_CustomerType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,CustomerTypeId")] Customer_CustomerType customer_CustomerType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_CustomerType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", customer_CustomerType.CustomerId);
            ViewBag.CustomerTypeId = new SelectList(db.CustomerTypes, "Id", "Name", customer_CustomerType.CustomerTypeId);
            return View(customer_CustomerType);
        }

        // GET: Customer_CustomerType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_CustomerType customer_CustomerType = db.Customer_CustomerTypes.Find(id);
            if (customer_CustomerType == null)
            {
                return HttpNotFound();
            }
            return View(customer_CustomerType);
        }

        // POST: Customer_CustomerType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer_CustomerType customer_CustomerType = db.Customer_CustomerTypes.Find(id);
            db.Customer_CustomerTypes.Remove(customer_CustomerType);
            db.SaveChanges();
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
