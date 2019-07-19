using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reserve_API.Models
{
    public class Animal
    {

        private List<AnimalPhoto> _photoList = new List<AnimalPhoto>();

        public int SpeciesID
        {
            get;
            set;
        }

        public string Species
        {
            get; set;
        }

        public AnimalDescription Description
        {
            get; set;
        }

        public int Population
        {
            get; set;
        }

        public List<AnimalPhoto> Photos
        {
            get
            {
                return _photoList;
            }
        }

        public Animal(int id, string species, int population, AnimalDescription ad)
        {
            SpeciesID = id;
            Species = species;
            Population = population;
            Description = ad;
        }

        public void AddPhoto(AnimalPhoto photo)
        {
            this._photoList.Add(photo);
        }

}
}