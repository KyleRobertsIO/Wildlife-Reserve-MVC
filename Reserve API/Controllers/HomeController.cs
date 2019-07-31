using MySql.Data.MySqlClient;
using Reserve_API.Database;
using Reserve_API.Database.Queries;
using Reserve_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            if (TempData["LoginErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["LoginErrorMessage"].ToString();
            }         
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Username, string Password)
        {
            DBUtil dbUtil = new DBUtil();
            MySqlDataAdapter adp = dbUtil.getConnectionSelect(
                $"SELECT username, password FROM accounts WHERE username = '{Username}'",
                "wildlife_reserve"
                );
            DataTable result = new DataTable();
            adp.Fill(result);

            if (result.Rows.Count > 0)
            {
                DataRow user = result.Rows[0];
                if (user["password"].ToString() == Password)
                {
                    Session["Username"] = Username;
                    return View("Index");
                }
                else
                {
                    //Wrong password
                    TempData["LoginErrorMessage"] = "Invalid password";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                // No result found
                TempData["LoginErrorMessage"] = "Invalid username";
                return RedirectToAction("Login");
            }
        }

        public ActionResult Creatures()
        {
            ViewBag.Title = "Creatures";

            return View();
        }

        public ActionResult Edit_Animal(int id = 0)
        {
            if (id == 0)
            {
                return View("Index");
            }
            if (Session["Username"] != null)
            {
                ViewBag.Title = "Edit Animal";
                try{
                    AnimalQueries aq = new AnimalQueries();
                    Animal animal = aq.SelectAnimalById(id);
                    return View(animal);
                }catch (Exception e)
                {
                    TempData["ErrorMessage"] = "The requested species can't be found.";
                    return RedirectToAction("Error");
                }
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult Edit_Animal_Update(int SpeciesID, int Population, string ShortDesc, string LongDesc)
        {

            if (Session["Username"] != null)
            {
                bool result = new AnimalQueries().UpdateAnimal(SpeciesID, Population, ShortDesc, LongDesc);
                if (result == false)
                {
                    TempData["ErrorMessage"] = "The edit you requested can't be performed. Please contact your administrator.";
                    return RedirectToAction("Error");
                }
                else
                {
                    ViewBag.Title = "Home";
                    return View("Index");
                }
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult Creature(int id)
        {
            try
            {
                CreatureQueries cq = new CreatureQueries();
                Creature creature = cq.SelectCreatureById(id);
                if (creature != null)
                {
                    return View(creature);
                }
                else
                {
                    TempData["ErrorMessage"] = "The requested creature cannot be found.";
                    return RedirectToAction("Error");
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "The requested creature cannot be found.";
                return RedirectToAction("Error");
            }
        }

        public ActionResult Creature_Delete(int id)
        {
            CreatureQueries cq = new CreatureQueries();
            bool result = cq.DeleteCreature(id);
            if (result == true)
            {
                return RedirectToAction("Creatures");
            }
            else
            {
                TempData["ErrorMessage"] = "The delete request couldn't be processed.";
                return RedirectToAction("Error");
            }
        }

        public ActionResult Error()
        {
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            return View("Error");
        }
    }
}
