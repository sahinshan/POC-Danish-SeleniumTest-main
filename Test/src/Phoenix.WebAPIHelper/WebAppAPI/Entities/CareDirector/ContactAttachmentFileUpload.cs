using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.CareDirector
{
    public class Contactid
    {
        public Guid id { get; set; }
    }
    public class Documenttypeid
    {
        public Guid id { get; set; }
    }
    public class Documentsubtypeid
    {
        public Guid id { get; set; }
    }
    public class ContactAttachment
    {
        public ContactAttachment()
        {
            contactid = new Contactid();
            documenttypeid = new Documenttypeid();
            documentsubtypeid = new Documentsubtypeid();
        }

        public Contactid contactid { get; set; }
        public Documenttypeid documenttypeid { get; set; }
        public Documentsubtypeid documentsubtypeid { get; set; }
        public string title { get; set; }
        public string date { get; set; }
    }
    public class ContactAttachmentFileUpload
    {
        public ContactAttachmentFileUpload()
        {
            this.record = new ContactAttachment();
        }

        public ContactAttachment record { get; set; }
        public string filename { get; set; }
        public string file { get; set; }
    }
}
