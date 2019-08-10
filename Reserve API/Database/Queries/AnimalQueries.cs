using MySql.Data.MySqlClient;
using Reserve_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Reserve_API.Database.Queries
{

    /*
        Author: Kyle 
    */

    public class AnimalQueries
    {

        public List<Animal> SelectAllAnimals()
        {
            List<Animal> animalList = new List<Animal>();
            DBUtil dbUtil = new DBUtil();
            MySqlDataAdapter adp = dbUtil.getConnectionSelect("SELECT * FROM animals_with_descriptions ORDER BY species_name ASC", "wildlife_reserve");
            DataTable animalTable = new DataTable();
            adp.Fill(animalTable);
            foreach (DataRow datarow in animalTable.Rows)
            {
                int speciesId = int.Parse(datarow["species_id"].ToString());
                string species = datarow["species_name"].ToString();
                int population = int.Parse(datarow["population"].ToString());
                string descLong = datarow["long_desc"].ToString();
                string descShort = datarow["short_desc"].ToString();
                DateTime breedStart = DateTime.Parse(datarow["breeding_season_start"].ToString());
                DateTime breedEnd = DateTime.Parse(datarow["breeding_season_end"].ToString());
                AnimalDescription description = new AnimalDescription(descLong, descShort, breedStart, breedEnd);
                Animal animal = new Animal(speciesId, species, population, description);

                MySqlDataAdapter adp2 = dbUtil.getConnectionSelect($"SELECT * FROM animal_images WHERE species_id = {speciesId}", "wildlife_reserve");
                DataTable photoTable = new DataTable();
                adp2.Fill(photoTable);

                foreach (DataRow datarow2 in photoTable.Rows)
                {
                    int photoId = int.Parse(datarow2["image_id"].ToString());
                    string fileName = datarow2["file_name"].ToString();
                    DateTime dateTaken = DateTime.Parse(datarow2["date_taken"].ToString());
                    AnimalPhoto animalPhoto = new AnimalPhoto(photoId, fileName, dateTaken);
                    animal.AddPhoto(animalPhoto);
                }

                animalList.Add(animal);
            }


            return animalList;
        }

        public Animal SelectAnimalById(int id)
        {
            Animal animal = null;
            DBUtil dbUtil = new DBUtil();
            MySqlDataAdapter adp = dbUtil.getConnectionSelect($"SELECT * FROM animals_with_descriptions WHERE species_id = {id}", "wildlife_reserve");
            DataTable table = new DataTable();
            adp.Fill(table);
            if (table.Rows.Count > 0)
            {
                DataRow datarow = table.Rows[0];
                int speciesId = int.Parse(datarow["species_id"].ToString());
                string species = datarow["species_name"].ToString();
                int population = int.Parse(datarow["population"].ToString());
                string descLong = datarow["long_desc"].ToString();
                string descShort = datarow["short_desc"].ToString();
                DateTime breedStart = DateTime.Parse(datarow["breeding_season_start"].ToString());
                DateTime breedEnd = DateTime.Parse(datarow["breeding_season_end"].ToString());
                AnimalDescription description = new AnimalDescription(descLong, descShort, breedStart, breedEnd);
                animal = new Animal(speciesId, species, population, description);

                MySqlDataAdapter adp2 = dbUtil.getConnectionSelect($"SELECT * FROM animal_images WHERE species_id = {speciesId}", "wildlife_reserve");
                DataTable photoTable = new DataTable();
                adp2.Fill(photoTable);

                foreach (DataRow datarow2 in photoTable.Rows)
                {
                    int photoId = int.Parse(datarow2["image_id"].ToString());
                    string fileName = datarow2["file_name"].ToString();
                    DateTime dateTaken = DateTime.Parse(datarow2["date_taken"].ToString());
                    AnimalPhoto animalPhoto = new AnimalPhoto(photoId, fileName, dateTaken);
                    animal.AddPhoto(animalPhoto);
                }
            }
            else
            {
                throw new NotFoundQueryException();
            }
            return animal;
        }

        public bool UpdateAnimal(int speciesId, int population, string shortDesc, string longDesc)
        {
            DBUtil dbUtil = new DBUtil();
            bool result1 = dbUtil.getConnectionUpdate($"UPDATE animals SET population = {population} WHERE species_id = {speciesId}", 
                "wildlife_reserve");

            bool result2 = dbUtil.getConnectionUpdate(
                $"UPDATE animal_descriptions SET short_desc = '{shortDesc}', long_desc = '{longDesc}' WHERE species_id = {speciesId}",
                "wildlife_reserve"
                );
            if (result1 && result2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}