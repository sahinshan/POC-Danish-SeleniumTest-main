using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.CareDirector
{
    public class Genderid
    {
        public string id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
    }

    public class Ownerid
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class PersonInfo
    {
        public Guid id { get; set; }
        public string fullname { get; set; }
        public string preferredname { get; set; }
        public string dateofbirth { get; set; }
        public Genderid genderid { get; set; }
        public string nhsnumber { get; set; }
        public string personnumber { get; set; }
        public string addressline1 { get; set; }
        public string addressline2 { get; set; }
        public string addressline3 { get; set; }
        public string addressline4 { get; set; }
        public string addressline5 { get; set; }
        public string postcode { get; set; }
        public Ownerid ownerid { get; set; }
    }

    public class PersonSearchResult
    {
        public bool hasMoreRecords { get; set; }
        public List<PersonInfo> data { get; set; }
    }


}
