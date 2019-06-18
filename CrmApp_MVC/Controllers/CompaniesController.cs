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
    public class CompaniesController : Controller
    {
        //private CRMAppContext db = new CRMAppContext();
        private ICompanyRepo CompanyRepo;
        public CompaniesController(ICompanyRepo _companyRepo)
        {
            this.CompanyRepo = _companyRepo;
        }

        // GET: Companies
        public ActionResult Index()
        {            
            return View(CompanyRepo.GetAllCompany());
        }

        // GET: Companies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = CompanyRepo.GetCompany(Convert.ToInt32(id));
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,PostAddress,Telephone,Email,Created")] Company company)
        {
            if (ModelState.IsValid)
            {
                CompanyRepo.AddNewCompany(company);                
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = CompanyRepo.GetCompany(Convert.ToInt32(id));
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,PostAddress,Telephone,Email,Created")] Company company)
        {
            if (ModelState.IsValid)
            {  
                CompanyRepo.UpdateCompany(company.Id,company);
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = CompanyRepo.GetCompany(Convert.ToInt32(id));
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CompanyRepo.RemoveCompany(id);
            return RedirectToAction("Index");
        }     
    }
}
