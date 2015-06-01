using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using FClam5.Models;

namespace FClam5.Controllers
{
    public class GoController : Controller
    {
        // GET: Go
        public ActionResult Index(String password)
        {
            using (var db = new ErrorReportContext())
            {
                db.ErrorReports.Add(new ErrorReport { reportNumber = 1, errorNumber = 15, errorType = "dumb", parentURL = "http://spsu.edu/", URL = "http://spsu.edu/broken" });
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult Run()
        {
            return View();
        }
    }
}