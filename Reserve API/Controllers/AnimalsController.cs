using Reserve_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Reserve_API.Database.Queries;

namespace Reserve_API.Controllers
{

    /*
        Author: Kyle 
    */

    public class AnimalsController : ApiController
    {

        public IEnumerable<Animal> Get()
        {
            return new AnimalQueries().SelectAllAnimals();
        }

        public Animal Get(int id)
        {
            return new AnimalQueries().SelectAnimalById(id);
        }

    }
}
