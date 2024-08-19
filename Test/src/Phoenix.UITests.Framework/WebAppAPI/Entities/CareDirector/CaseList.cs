using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Casestatusid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Contactreasonid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Contactreceivedbyid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Contactsourceid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Createdby
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Dataformid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Modifiedby
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Ownerid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Personawareofcontactid
    {
        public string id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
    }

    public class Personid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Responsibleuserid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Personsupportcontactid
    {
        public string id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
    }

    public class Contactmadebyid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Administrativecategoryid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Caseacceptedid
    {
        public string id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
    }

    public class Casepriorityid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Communityandclinicteamid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Servicetyperequestedid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Nextofkinawareofcontactid
    {
        public string id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
    }

    public class Childinneedcodeid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Caseclosurereasonid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Caseoriginid
    {
        public string id { get; set; }
        public string name { get; set; }
        public object code { get; set; }
    }

    public class Closureacceptedbyid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Presentingpriorityid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Case
    {
        public string id { get; set; }
        public string carernoknotified { get; set; }
        public string casenumber { get; set; }
        public Casestatusid casestatusid { get; set; }
        public Contactreasonid contactreasonid { get; set; }
        public Contactreceivedbyid contactreceivedbyid { get; set; }
        public string contactreceiveddatetime { get; set; }
        public Contactsourceid contactsourceid { get; set; }
        public Createdby createdby { get; set; }
        public string createdon { get; set; }
        public Dataformid dataformid { get; set; }
        public string dischargeperson { get; set; }
        public string inactive { get; set; }
        public string iscloned { get; set; }
        public string ispersononleave { get; set; }
        public string isswappinginpatient { get; set; }
        public Modifiedby modifiedby { get; set; }
        public string modifiedon { get; set; }
        public Ownerid ownerid { get; set; }
        public string personage { get; set; }
        public Personawareofcontactid personawareofcontactid { get; set; }
        public Personid personid { get; set; }
        public string policenotified { get; set; }
        public string rereferral { get; set; }
        public string responsemadetocontact { get; set; }
        public Responsibleuserid responsibleuserid { get; set; }
        public string section117aftercareentitlement { get; set; }
        public string startdatetime { get; set; }
        public string title { get; set; }
        public Personsupportcontactid personsupportcontactid { get; set; }
        public Contactmadebyid contactmadebyid { get; set; }
        public Administrativecategoryid administrativecategoryid { get; set; }
        public string caseaccepteddatetime { get; set; }
        public Caseacceptedid caseacceptedid { get; set; }
        public Casepriorityid casepriorityid { get; set; }
        public string cnacount { get; set; }
        public Communityandclinicteamid communityandclinicteamid { get; set; }
        public string dnacount { get; set; }
        public string presentingneeddetails { get; set; }
        public string requestreceiveddatetime { get; set; }
        public Servicetyperequestedid servicetyperequestedid { get; set; }
        public Nextofkinawareofcontactid nextofkinawareofcontactid { get; set; }
        public string additionalinformation { get; set; }
        public Childinneedcodeid childinneedcodeid { get; set; }
        public string archivedate { get; set; }
        public Caseclosurereasonid caseclosurereasonid { get; set; }
        public Caseoriginid caseoriginid { get; set; }
        public Closureacceptedbyid closureacceptedbyid { get; set; }
        public string contactmadebyname { get; set; }
        public string enddatetime { get; set; }
        public Presentingpriorityid presentingpriorityid { get; set; }
    }

    public class CaseList
    {
        public string hasMoreRecords { get; set; }
        public List<Case> data { get; set; }
    }


}
