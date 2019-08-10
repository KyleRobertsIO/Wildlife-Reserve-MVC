using Reserve_API.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reserve_API.Database.Queries
{

    public class CreatureQueries
    {
        // Kyle
        public List<Creature> SelectAllCreatures()
        {
            List<Creature> creatureList = new List<Creature>();
            DBUtil dbUtil = new DBUtil();
            MySqlDataAdapter adp = dbUtil.getConnectionSelect(
                "SELECT * FROM creatures",
                "wildlife_reserve"
            );
            DataTable creatureTable = new DataTable();
            adp.Fill(creatureTable);
            foreach (DataRow datarow in creatureTable.Rows)
            {
                int creatureId = int.Parse(datarow["creature_id"].ToString());
                int speciesId = int.Parse(datarow["species_id"].ToString());
                string nickname = datarow["nickname"].ToString();
                // Query Animal data
                AnimalQueries aq = new AnimalQueries();
                Animal species = aq.SelectAnimalById(speciesId);
                // Create creature object
                Creature creature = new Creature(creatureId, nickname, species);

                MySqlDataAdapter adp2 = dbUtil.getConnectionSelect(
                    $"SELECT * FROM medical_records WHERE creature_id = {creatureId}",
                    "wildlife_reserve"
                );
                DataTable recordsTable = new DataTable();
                adp2.Fill(recordsTable);

                foreach(DataRow datarow2 in recordsTable.Rows)
                {
                    int recordId = int.Parse(datarow2["record_id"].ToString());
                    string vetrainarian = datarow2["vet_name"].ToString();
                    string procedureDesc = datarow2["procedure_desc"].ToString();
                    DateTime datePerformed = DateTime.Parse(datarow2["date_performed"].ToString());
                    
                    MedicalRecord record = new MedicalRecord(
                        recordId, vetrainarian, procedureDesc, datePerformed);
                    creature.AddMedicalRecord(record);
                }
                creatureList.Add(creature);
            }
            return creatureList;
        }


        // Kyle
        public Creature SelectCreatureById(int id)
        {
            Creature creature = null;
            DBUtil dbUtil = new DBUtil();
            MySqlDataAdapter adp = dbUtil.getConnectionSelect(
                $"SELECT * FROM creatures WHERE creature_id = {id}",
                "wildlife_reserve"
            );
            DataTable creatureTable = new DataTable();
            adp.Fill(creatureTable);
            if (creatureTable.Rows.Count > 0)
            {
                DataRow datarow = creatureTable.Rows[0];
                int speciesId = int.Parse(datarow["species_id"].ToString());
                string nickname = datarow["nickname"].ToString();

                // Query Animal data
                AnimalQueries aq = new AnimalQueries();
                Animal species = aq.SelectAnimalById(speciesId);
                // Create creature object
                creature = new Creature(id, nickname, species);

                MySqlDataAdapter adp2 = dbUtil.getConnectionSelect(
                    $"SELECT * FROM medical_records WHERE creature_id = {id}",
                    "wildlife_reserve"
                );
                DataTable recordsTable = new DataTable();
                adp2.Fill(recordsTable);

                foreach (DataRow datarow2 in recordsTable.Rows)
                {
                    int recordId = int.Parse(datarow2["record_id"].ToString());
                    string vetrainarian = datarow2["vet_name"].ToString();
                    string procedureDesc = datarow2["procedure_desc"].ToString();
                    DateTime datePerformed = DateTime.Parse(datarow2["date_performed"].ToString());

                    MedicalRecord record = new MedicalRecord(
                        recordId, vetrainarian, procedureDesc, datePerformed);
                    creature.AddMedicalRecord(record);
                }
            }
            else
            {
                throw new NotFoundQueryException();
            }
            return creature;
        }

        // Kyle
        public bool DeleteCreature(int id)
        {
            bool result = false;
            DBUtil dbUtil = new DBUtil();
            result = dbUtil.getConnectionDelete(
                $"DELETE FROM medical_records WHERE creature_id = {id}; " +
                $"DELETE FROM creatures WHERE creature_id = {id}",
                "wildlife_reserve");
            return result;
        }


        // Kyle
        public bool AddCreature(string nickname, int speciesId)
        {
            bool result = false;
            DBUtil dbUtil = new DBUtil();
            result = dbUtil.getConnectionExecute(
                $"INSERT INTO creatures (nickname, species_id) VALUES ('{nickname}', '{speciesId}')",
                "wildlife_reserve");
            return result;
        }
		
        // Huma
		public bool UpdateCreature(string nickname, int creatureId, int speciesId)
        {
            DBUtil dbUtil = new DBUtil();
            bool result = dbUtil.getConnectionUpdate($"UPDATE creatures SET nickname = '{nickname}', species_id = {speciesId} WHERE record_id = {creatureId}",
                "wildlife_reserve");
            return result;
        }
		
    }
}