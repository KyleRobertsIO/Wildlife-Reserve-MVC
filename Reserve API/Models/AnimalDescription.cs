using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reserve_API.Models
{

    /*
        Author: Kyle
    */

    public class AnimalDescription
    {

        public string DescriptionLong
        {
            get; set;
        }

        public string DescriptionShort
        {
            get; set;
        }

        public DateTime BreedingSeasonStart
        {
            get; set;
        }

        public DateTime BreedingSeasonEnd
        {
            get; set;
        }

        public AnimalDescription(string descLong, string descShort, 
            DateTime breedStart, DateTime breedEnd)
        {
            DescriptionLong = descLong;
            DescriptionShort = descShort;
            BreedingSeasonStart = breedStart;
            BreedingSeasonEnd = breedEnd;
        }
    }
}