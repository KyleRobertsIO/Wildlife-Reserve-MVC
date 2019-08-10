using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reserve_API.Database.Queries
{

    /*
        Author: Kyle 
    */

    class NotFoundQueryException : Exception
    {
        public NotFoundQueryException() : base("The requested query returned no results.")
        {

        }
    }
}