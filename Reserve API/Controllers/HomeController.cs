using Reserve_API.Database;
using Reserve_API.Database.Queries;
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

        public ViewResult Edit_Animal()
        {
            ViewBag.Title = "Edit Animal";
            return View();
        }

        public class EditAnimalFormModel
        {
            public int SpeciesID { get; set; }
            public int Population { get; set; }
            public string ShortDesc { get; set; }
            public string LongDesc { get; set; }
        }

        [HttpPut]
        public ActionResult Edit_Animal(EditAnimalFormModel model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Title = "Home";
                int speciesId = model.SpeciesID;
                int population = model.Population;
                string shortDesc = model.ShortDesc;
                string longDesc = model.LongDesc;
                bool result = new AnimalQueries().UpdateAnimal(speciesId, population);
                if (result == false)
                {
                    return View("Error");
                }
                else
                {
                    return View("Index");
                }
            }
            else
            {
                ViewBag.Title = "Edit Animal";
                return View();
            }
        }
    }
}
