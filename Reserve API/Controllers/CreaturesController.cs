using Reserve_API.Database.Queries;
using Reserve_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Reserve_API.Controllers
{

    /*
        Author: Kyle 
    */

    public class CreaturesController : ApiController
    {
        
        public IEnumerable<Creature> Get()
        {
            return new CreatureQueries().SelectAllCreatures();
        }

    }
}
