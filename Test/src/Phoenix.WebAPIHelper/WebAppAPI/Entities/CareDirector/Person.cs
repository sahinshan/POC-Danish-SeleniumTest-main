using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.CareDirector
{
    
    public class Accommodationstatusid
    {
        public int id { get; set; }
    }

    public class Ethnicityid
    {
        public Guid id { get; set; }
    }

    public class Person
    {
        public Accommodationstatusid accommodationstatusid { get; set; }
        
        public Ethnicityid ethnicityid { get; set; }
        
        public bool? AllergiesNotRecorded { get; set; }
    }



}
