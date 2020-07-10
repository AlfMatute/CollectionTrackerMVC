using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CollectionTrackerMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Collection Tracker V1";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Alfredo's contact information.";

            return View();
        }
    }
}