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

    }
}