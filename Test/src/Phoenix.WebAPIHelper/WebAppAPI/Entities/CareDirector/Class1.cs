using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.CareDirector
{

    public class Assignedteamid
    {
        public string id { get; set; }
    }

    public class Carerawareofcontactid
    {
        public int id { get; set; }
    }

    public class Carersupportcontactid
    {
        public string id { get; set; }
    }

    public class Childinneedcodeid
    {
        public string id { get; set; }
    }

    public class Communityandclinicteamid
    {
        public string id { get; set; }
    }

    public class Consultantid
    {
        public string id { get; set; }
    }

    public class Contactaccessrouteid
    {
        public string id { get; set; }
    }

    public class Contactadministrativecategoryid
    {
        public string id { get; set; }
    }

    public class Contactmadebyid
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Contactoutcomeid
    {
        public string id { get; set; }
    }

    public class Contactpresentingpriorityid
    {
        public Guid id { get; set; }
    }

    public class Contactrejectionreasonid
    {
        public string id { get; set; }
    }

    public class Contactreopenreasonid
    {
        public string id { get; set; }
    }

    public class Contactreviewreasonid
    {
        public string id { get; set; }
    }

    public class Contactsequeltoreviewid
    {
        public string id { get; set; }
    }

    public class Contactsequeltostmaxid
    {
        public string id { get; set; }
    }

    public class Contactsourceid
    {
        public string id { get; set; }
    }

    public class Contactstatusid
    {
        public Guid id { get; set; }
    }

    public class Contacttypeid
    {
        public Guid id { get; set; }
    }

    public class FinAccomodationtypeid
    {
        public string id { get; set; }
    }

    public class Grouprecordid
    {
        public string id { get; set; }
    }

    public class Inpatientwardid
    {
        public string id { get; set; }
    }

    public class Nextofkinawareofcontactid
    {
        public int id { get; set; }
    }

    public class Persongroupawareofcontactid
    {
        public int id { get; set; }
    }

    public class Persongroupsupportcontactid
    {
        public string id { get; set; }
    }

    public class Personid
    {
        public Guid id { get; set; }
    }

    public class Providerid
    {
        public Guid id { get; set; }
    }

    public class Reasonforoutofareareferralid
    {
        public string id { get; set; }
    }

    public class Referralpriorityid
    {
        public Guid id { get; set; }
    }

    public class Regardingid
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Responsibleuserid
    {
        public string id { get; set; }
    }

    public class Sequeltocontactid
    {
        public string id { get; set; }
    }

    public class ContactBO
    {
        public FinAccomodationtypeid fin_accomodationtypeid { get; set; }
        public string additionalinformation { get; set; }
        public Contactadministrativecategoryid contactadministrativecategoryid { get; set; }
        public Childinneedcodeid childinneedcodeid { get; set; }
        public Communityandclinicteamid communityandclinicteamid { get; set; }
        public string consentdatetime { get; set; }
        public string consentername { get; set; }
        public Contactmadebyid contactmadebyid { get; set; }
        public string contactmadebyfreetext { get; set; }
        public Contactoutcomeid contactoutcomeid { get; set; }
        public Contactreasonid contactreasonid { get; set; }
        public string contactrejectionqueries { get; set; }
        public Contactrejectionreasonid contactrejectionreasonid { get; set; }
        public string contactrejectionresponse { get; set; }
        public Contactsourceid contactsourceid { get; set; }
        public Contactstatusid contactstatusid { get; set; }
        public string contactsummary { get; set; }
        public Contacttypeid contacttypeid { get; set; }
        public string country { get; set; }
        public Consultantid consultantid { get; set; }
        public string datesequelknown { get; set; }
        public string datesequeltostmaxknown { get; set; }
        public string contactaccepteddatetime { get; set; }
        public string contactassigneddatetime { get; set; }
        public string contactcloseddatetime { get; set; }
        public string contactreceiveddatetime { get; set; }
        public string contactrejecteddatetime { get; set; }
        public string department { get; set; }
        public string isdatacollectionandusageconsent { get; set; }
        public Carersupportcontactid carersupportcontactid { get; set; }
        public Persongroupsupportcontactid persongroupsupportcontactid { get; set; }
        public string firstname { get; set; }
        public Grouprecordid grouprecordid { get; set; }
        public string homephone { get; set; }
        public Providerid providerid { get; set; }
        public string intendedadmissiondate { get; set; }
        public Nextofkinawareofcontactid nextofkinawareofcontactid { get; set; }
        public Carerawareofcontactid carerawareofcontactid { get; set; }
        public Persongroupawareofcontactid persongroupawareofcontactid { get; set; }
        public string lastname { get; set; }
        public string mobilephone { get; set; }
        public string otheractions { get; set; }
        public Personid personid { get; set; }
        public string position { get; set; }
        public string postcode { get; set; }
        public string presentingneed { get; set; }
        public Contactpresentingpriorityid contactpresentingpriorityid { get; set; }
        public string primaryemail { get; set; }
        public Referralpriorityid referralpriorityid { get; set; }
        public string propertynumber { get; set; }
        public Reasonforoutofareareferralid reasonforoutofareareferralid { get; set; }
        public Contactreopenreasonid contactreopenreasonid { get; set; }
        public Contactreviewreasonid contactreviewreasonid { get; set; }
        public Regardingid regardingid { get; set; }
        public Ownerid ownerid { get; set; }
        public Responsibleuserid responsibleuserid { get; set; }
        public Inpatientwardid inpatientwardid { get; set; }
        public Contactaccessrouteid contactaccessrouteid { get; set; }
        public Sequeltocontactid sequeltocontactid { get; set; }
        public Contactsequeltoreviewid contactsequeltoreviewid { get; set; }
        public Contactsequeltostmaxid contactsequeltostmaxid { get; set; }
        public string street { get; set; }
        public Assignedteamid assignedteamid { get; set; }
        public string title { get; set; }
        public string city { get; set; }
        public string district { get; set; }
    }




}
