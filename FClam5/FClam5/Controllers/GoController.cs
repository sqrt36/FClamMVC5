using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FClam5.Controllers
{
    public class GoController : Controller
    {
        // GET: Go
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Run()
        {
            return View();
        }
        public String Test()
        {

            return "It's working";
        }
    }
}