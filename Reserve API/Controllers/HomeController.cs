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

        public ViewResult Login()
        {
            ViewBag.Title = "Login";
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
                    return View("Login");
                }
            }
            else
            {
                // No result found
                return View("Login");
            }
        }

        public ActionResult Creatures()
        {
            ViewBag.Title = "Creatures";

            return View();
        }

        public ViewResult Edit_Animal(int id)
        {
            if (Session["Username"] != null)
            {
                ViewBag.Title = "Edit Animal";
                AnimalQueries aq = new AnimalQueries();
                Animal animal = aq.SelectAnimalById(id);
                return View(animal);
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
                    ViewBag.Title = "Error";
                    return View("Error");
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
    }
}
