using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.CareDirector
{
    public class Contactreasonid
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Contact
    {
        public Guid id { get; set; }
        public string contactreceiveddatetime { get; set; }
        public Contactreasonid contactreasonid { get; set; }
        public Ownerid ownerid { get; set; }
    }

    public class ChiePersonContactsSearchData
    {
        public string hasMoreRecords { get; set; }
        public List<Contact> data { get; set; }
    }


}
