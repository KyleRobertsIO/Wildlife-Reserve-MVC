using MySql.Data.MySqlClient;
using Reserve_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Reserve_API.Database.Queries
{
    public class MedicalRecordQueries
    {
		// Huma
		public List<MedicalRecord> SelectAllMedicalRecords(int creatureId)
        {
            DBUtil dbUtil = new DBUtil();
            List<MedicalRecord> records = new List<MedicalRecord>();
            MySqlDataAdapter adp = dbUtil.getConnectionSelect($"SELECT * FROM medical_records WHERE creature_id = {creatureId}", "wildlife_reserve");
            DataTable recordsTable = new DataTable();
            adp.Fill(recordsTable);

            foreach (DataRow datarow in recordsTable.Rows)
            {
                int recordId = int.Parse(datarow["record_id"].ToString());
                string vetrainarian = datarow["vet_name"].ToString();
                string procedureDesc = datarow["procedure_desc"].ToString();
                DateTime datePerformed = DateTime.Parse(datarow["date_performed"].ToString());

                MedicalRecord record = new MedicalRecord(
                    recordId, vetrainarian, procedureDesc, datePerformed);
                records.Add(record);
            }
            return records;
        }
		
		// Kyle
        public MedicalRecord SelectMedicalRecordById(int id)
        {
            MedicalRecord medicalRecord = null;
            DBUtil dbUtil = new DBUtil();
            MySqlDataAdapter adp = dbUtil.getConnectionSelect(
                $"SELECT * FROM medical_records WHERE record_id = '{id}'",
                "wildlife_reserve"
            );
            DataTable medRecTable = new DataTable();
            adp.Fill(medRecTable);
            if (medRecTable.Rows.Count > 0)
            {
                DataRow row = medRecTable.Rows[0];
                int recordId = int.Parse(row["record_id"].ToString());
                string vetName = row["vet_name"].ToString();
                string description = row["procedure_desc"].ToString();
                DateTime datePerformed = DateTime.Parse(
                    row["date_performed"].ToString()
                );
                medicalRecord = new MedicalRecord(
                    recordId,
                    vetName,
                    description,
                    datePerformed
                );
            }
            else
            {
                throw new NotFoundQueryException();
            }
            return medicalRecord;
        }
		
		// Huma
		public bool InsertMedicalRecord(string vetName, string procedureDescription, string datePerformed, int creatureId)
        {
            bool result = false;
            DBUtil dbUtil = new DBUtil();
            result = dbUtil.getConnectionExecute(
                $"INSERT INTO medical_records " +
                $"(vet_name, procedure_desc, date_performed, creature_id) " +
                $"VALUES " +
                $"('{vetName}', '{procedureDescription}', '{datePerformed}', {creatureId})",
                "wildlife_reserve");
            return result;
        }
		
		//Huma
        public bool UpdateMedicalRecord(int recordId, string vetName, string procedureDescription, string datePerformed, int creatureId)
        {
            DBUtil dbUtil = new DBUtil();
            bool result = dbUtil.getConnectionUpdate(
                $"UPDATE medical_records SET creature_id = {creatureId}, " +
                $"vet_name = '{vetName}', procedure_desc = '{procedureDescription}', " +
                $"date_performed = '{datePerformed}' WHERE record_id = {recordId}",
                "wildlife_reserve"
            );

            return result;
        }
		
		// Huma
        public bool DeleteMedicalRecord(int id)
        {
            bool result = false;
            DBUtil dbUtil = new DBUtil();
            result = dbUtil.getConnectionDelete(
                $"DELETE FROM medical_records WHERE creature_id = {id};",
                "wildlife_reserve");
            return result;
        }
		
    }
}