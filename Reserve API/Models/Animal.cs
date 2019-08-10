using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reserve_API.Models
{

    /*
        Author: Kyle
    */

    public class Animal
    {
        
        private List<AnimalPhoto> _photoList = new List<AnimalPhoto>();

        [Required]
        public int SpeciesID
        {
            get;
            set;
        }

        [Required]
        public string Species
        {
            get; set;
        }

        [Required]
        public AnimalDescription Description
        {
            get; set;
        }

        [Required]
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