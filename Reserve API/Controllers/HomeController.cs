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

    /*
        Author: Kyle 
    */

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
                try {
                    AnimalQueries aq = new AnimalQueries();
                    Animal animal = aq.SelectAnimalById(id);
                    return View(animal);
                } catch (Exception e)
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

        [HttpPost]
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
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login");
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
                    ViewBag.Title = $"{creature.Nickname} | {creature.Species}";
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

        [HttpDelete]
        public ActionResult Creature_Delete(int id)
        {
            if (Session["Username"] != null)
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
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Creature_Add()
        {
            if (Session["Username"] != null)
            {
                ViewBag.Title = "Add Creature";
                DBUtil dbUtil = new DBUtil();
                MySqlDataAdapter adp = dbUtil.getConnectionSelect(
                    "SELECT species_id, species_name FROM animals ORDER BY species_name ASC",
                    "wildlife_reserve"
                );
                DataTable table = new DataTable();
                adp.Fill(table);
                Dictionary<int, string> speciesList = new Dictionary<int, string>();
                foreach (DataRow row in table.Rows)
                {
                    int speciesID = int.Parse(row["species_id"].ToString());
                    string speciesName = row["species_name"].ToString();
                    speciesList.Add(speciesID, speciesName);
                }
                ViewBag.SpeciesDictionary = speciesList;
                if (TempData["ErrorMessage"] != null)
                {
                    ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult Creature_Add_Insert(string Nickname, int SpeciesID)
        {
            if (Session["Username"] != null)
            {
                if (Nickname == "")
                {
                    TempData["ErrorMessage"] = "Provide a nickname";
                    return RedirectToAction("Creature_Add");
                }
                try
                {
                    CreatureQueries cq = new CreatureQueries();
                    bool result = cq.AddCreature(Nickname, SpeciesID);
                    if (result == true)
                    {
                        return RedirectToAction("Creatures");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "The creature provided can't be inserted at this time.";
                        return RedirectToAction("Creature_Add");
                    }
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = "Something went wrong. Please try adding again.";
                    return View("Error");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Medical_Record(int id)
        {
            MedicalRecord medRecord = null;
            DBUtil dbUtil = new DBUtil();
            MySqlDataAdapter adp = dbUtil.getConnectionSelect(
                $"SELECT * FROM creature_with_medical_records WHERE record_id = '{id}'",
                "wildlife_reserve"
            );
            DataTable recordTable = new DataTable();
            adp.Fill(recordTable);
            if (recordTable.Rows.Count > 0)
            {
                DataRow row = recordTable.Rows[0];
                int recordId = int.Parse(row["record_id"].ToString());
                string vetName = row["vet_name"].ToString();
                string description = row["procedure_desc"].ToString();
                DateTime datePerformed = DateTime.Parse(
                    row["date_performed"].ToString()
                );
                medRecord = new MedicalRecord(
                    recordId,
                    vetName,
                    description,
                    datePerformed
                );
                string nickname = row["nickname"].ToString();
                ViewBag.Nickname = nickname;
                return View("Medical_Record", medRecord);
            }
            else
            {
                TempData["ErrorMessage"] = "The medical record requested can't be found.";
                return RedirectToAction("Error");
            }
        }

        public ActionResult Edit_Medical_Record(int id)
        {
            if(Session["Username"] != null)
            {
                try
                {
                    MedicalRecordQueries mrq = new MedicalRecordQueries();
                    MedicalRecord record = mrq.SelectMedicalRecordById(id);
                    return View(record);
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = "The requested medical record can't be found.";
                    return RedirectToAction("Error");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult Edit_Medical_Record_Update(int RecordID, string Veterinarian, string ProcedureDescription)
        {
            if(Session["Username"] != null)
            {
                bool result = new MedicalRecordQueries().UpdateMedicalRecord(RecordID, Veterinarian, ProcedureDescription);

                if (result == false)
                {
                    TempData["ErrorMessage"] = "The edit you requested can't be performed. Please contact your administrator.";
                    return RedirectToAction("Error");
                }
                else
                {
                    return RedirectToAction("Creatures");
                }
            }
            else
            {
                return RedirectToAction("Login");
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
