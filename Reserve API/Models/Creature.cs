using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reserve_API.Models
{

    /*
        Author: Huma 
    */
    public class Creature
    {

        private List<MedicalRecord> _medicalRecords = new List<MedicalRecord>();

        public int CreatureID
        {
            get; set;
        }

        public string Nickname
        {
            get; set;
        }

        public Animal Species
        {
            get; set;
        }

        public List<MedicalRecord> MedicalRecords
        {
            get
            {
                return _medicalRecords;
            }
        }

        public Creature(int id, string nickname, Animal species)
        {
            this.CreatureID = id;
            this.Nickname = nickname;
            this.Species = species;
        }

        public void AddMedicalRecord(MedicalRecord record)
        {
            this._medicalRecords.Add(record);
        }

    }
}