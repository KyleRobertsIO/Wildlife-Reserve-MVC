using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reserve_API.Models
{
    public class AnimalPhoto
    {

        public int PhotoId
        {
            get; set;
        }

        public string FileName
        {
            get; set;
        }

        public DateTime DateTaken
        {
            get; set;
        }

        public AnimalPhoto(int id, string fileName, DateTime dateTaken)
        {
            PhotoId = id;
            FileName = fileName;
            DateTaken = dateTaken;
        }

    }
}