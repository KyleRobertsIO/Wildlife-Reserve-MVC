using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reserve_API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home";

            return View();
        }

        public ActionResult Animals()
        {
            ViewBag.Title = "Animals";

            return View();
        }

        public ActionResult Creatures()
        {
            ViewBag.Title = "Creatures";

            return View();
        }

    }
}
