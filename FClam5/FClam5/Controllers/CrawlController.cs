using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FClam5.Models;

namespace FClam5.Controllers
{
    public class CrawlController : Controller
    {
        private ErrorReportContext db = new ErrorReportContext();

        // GET: Crawl
        public ActionResult Index()
        {
            return View(db.ErrorReports.ToList());
        }

        // GET: Crawl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorReport errorReport = db.ErrorReports.Find(id);
            if (errorReport == null)
            {
                return HttpNotFound();
            }
            return View(errorReport);
        }

        // GET: Crawl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Crawl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reportNumber,errorNumber,URL,parentURL,errorType")] ErrorReport errorReport)
        {
            if (ModelState.IsValid)
            {
                db.ErrorReports.Add(errorReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(errorReport);
        }

        // GET: Crawl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorReport errorReport = db.ErrorReports.Find(id);
            if (errorReport == null)
            {
                return HttpNotFound();
            }
            return View(errorReport);
        }

        // POST: Crawl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reportNumber,errorNumber,URL,parentURL,errorType")] ErrorReport errorReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(errorReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(errorReport);
        }

        // GET: Crawl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorReport errorReport = db.ErrorReports.Find(id);
            if (errorReport == null)
            {
                return HttpNotFound();
            }
            return View(errorReport);
        }

        // POST: Crawl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ErrorReport errorReport = db.ErrorReports.Find(id);
            db.ErrorReports.Remove(errorReport);
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
