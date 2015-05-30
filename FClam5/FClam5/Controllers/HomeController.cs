using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FClam5.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index(String password)
        {
            if (password == null)
                return View();
            if (password.Equals("blueberry"))
                return View("DoStuff");
            return View("LoginFail");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}