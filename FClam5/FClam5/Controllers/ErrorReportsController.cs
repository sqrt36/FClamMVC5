using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FClam5.Models;
using System.Threading.Tasks;
using System.IO;
using HtmlAgilityPack;

namespace FClam5.Controllers
{
    public class ErrorReportsController : Controller
    {
        private ErrorReportContext db = new ErrorReportContext();

        // GET: ErrorReports
        public ActionResult Index()
        {
            return View(db.ErrorReports.ToList());
        }

        // GET: ErrorReports/Details/5
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

        // GET: ErrorReports/Create
        public ActionResult Create()
        {
            return View();
        }
        
        public ActionResult Populate()
        {
            Console.WriteLine("I wonder where this will appear.");
            //crawler goes here
            //int reportNumber, errorNumber;
            //String URL, parentURL;
            //ErrorReportContext errorReport = new ErrorReportContext();
            //ErrorReport error = new ErrorReport();
            //String[] url = { "http://spsu.edu/admissions", "http://spsu.edu/campuslife", "http://spsu.edu/owlssuck", "http://spsu.edu/engineering", "http://spsu.edu/dean" };

            //for (int i = 20; i < 25; i++)
            //{
            //    error.reportNumber = i;
            //    error.errorNumber = i;
            //    error.URL = url[i-20];
            //    error.parentURL = "http://spsu.edu/";
            //    error.errorType = "broke";
            //    errorReport.ErrorReports.Add(error);
            //    errorReport.SaveChanges();
            //}
            return View("Index");

        }

        public void Crawl()
        {
            string current_page = "";
            string domain = "spsu.edu";
            string start_page = "https://" + domain;
            List<string> to_search = new List<string>();
            List<string> searched = new List<string>();
            List<string> parents = new List<string>();
            string puppies = "";
            string href = "";
            string content = "";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(start_page);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader stream = new StreamReader(response.GetResponseStream());
            string source_code = stream.ReadToEnd();
            HtmlDocument doc = new HtmlDocument();
            doc.OptionReadEncoding = false;
            to_search.Add(start_page);
            int errorNumber = 0;
            ErrorReportContext db = new ErrorReportContext();

            while (to_search.Count > 0)
            {
                current_page = to_search.ElementAt(0);
                to_search.RemoveAt(0);
                if (parents.Count() != 0)
                    parents.RemoveAt(0);
                //foreach (string link in searched) // add things to searched
                //    if (link == current_page)
                //        continue;
                if (searched.Contains(current_page))
                    continue;
                //request = (HttpWebRequest)HttpWebRequest.Create(current_page);
                //request.Method = "HEAD";
                ////response.Dispose();
                //try
                //{
                //    response = (HttpWebResponse)request.GetResponse();
                //    //request.EndGetResponse();

                //}
                //catch (System.Net.WebException ex)
                //{
                //    var status = response.StatusCode;
                //    switch(status.ToString())
                //    {
                //        case "NotFound":
                //            Console.WriteLine("Huzzah!");
                //            break;
                //    }
                //    Console.WriteLine(ex);
                //    continue;
                //}
                //var status_code = response.StatusCode;
                //Console.WriteLine(status_code.ToString());
                try
                {
                    request = (HttpWebRequest)HttpWebRequest.Create(current_page);
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (System.Net.WebException ex)
                {
                    //insert error here
                    db.ErrorReports.Add(new ErrorReport { reportNumber = 2, errorNumber = errorNumber++, URL = current_page, parentURL = parents.ElementAt(0), errorType = ex.Source.ToString() });
                    db.SaveChanges();
                    Console.WriteLine(ex.Status);
                    continue;
                }
                var status = response.StatusCode;
                Console.WriteLine(status);
                searched.Add(current_page);
                content = response.ContentType;
                if (!(content.Contains("text/html") || content.Contains("application/xhtml+xml") || content.Contains("application/xml")))
                {
                    Console.WriteLine("Invalid Content type " + current_page);
                    continue;
                }
                //if (!current_page.Contains(domain)) //use host?
                //{
                //    Console.WriteLine("Off domain " + current_page);
                //    continue;
                //}
                if (!request.Host.Equals(domain))
                {
                    Console.WriteLine("Off domain " + current_page);
                    continue;
                }
                // start to parse html 
                stream = new StreamReader(response.GetResponseStream());
                source_code = stream.ReadToEnd();
                doc.LoadHtml(source_code);
                Console.WriteLine(current_page);
                var links = doc.DocumentNode.SelectNodes("//a[@href]");
                if (links != null)
                {
                    foreach (HtmlNode link in links)
                    {
                        HtmlAttribute att = link.Attributes["href"];
                        href = att.Value;
                        if (href == null)
                            break;
                        else if (href == "")
                            break;
                        else if (href.Contains("mailto:"))
                            break;
                        else if (href.ElementAt(0) == '/')
                            puppies = start_page + href;
                        else if (href.ElementAt(0) == '?')
                            puppies = start_page + href;
                        else if (href.ElementAt(0) == '#')
                            break;
                        else if (href.Contains("http"))
                            puppies = href;
                        else if (href.Contains("javascript"))
                            break;
                        else puppies = start_page + href;
                        to_search.Add(puppies);
                        parents.Add(current_page);
                        //Console.WriteLine(puppies);
                    }
                }
                response.Close();
            }
        }

        // POST: ErrorReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "errorNumber,reportNumber,URL,parentURL,errorType")] ErrorReport errorReport)
        {
            if (ModelState.IsValid)
            {
                db.ErrorReports.Add(errorReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(errorReport);
        }

        // GET: ErrorReports/Edit/5
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

        // POST: ErrorReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "errorNumber,reportNumber,URL,parentURL,errorType")] ErrorReport errorReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(errorReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(errorReport);
        }

        // GET: ErrorReports/Delete/5
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

        // POST: ErrorReports/Delete/5
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
