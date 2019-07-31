using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reserve_API.Models
{
    public class MedicalRecord
    {

        public int RecordID
        {
            get; set;
        }

        public string Veterinarian
        {
            get; set;
        }

        public string ProcedureDescription
        {
            get; set;
        }

        public DateTime DatePerformed
        {
            get; set;
        }

        public MedicalRecord(int id, string vet, string procedure, DateTime datePerformed)
        {
            this.RecordID = id;
            this.Veterinarian = vet;
            this.ProcedureDescription = procedure;
            this.DatePerformed = datePerformed;
        }

    }
}