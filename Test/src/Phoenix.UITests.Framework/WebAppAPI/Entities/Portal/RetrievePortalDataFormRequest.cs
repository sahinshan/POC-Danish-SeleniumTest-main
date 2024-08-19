using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.Portal
{
    public class RetrievePortalDataFormRequest
    {
        public Guid? DataFormId { get; set; }
        public string DataFormName { get; set; }
        public string BusinessObjectName { get; set; }
        public Guid? RecordId { get; set; }
        public Guid? RelationshipId { get; set; }
        public bool FirstView { get; set; }
        public Guid WebsiteId { get; set; }        
        public PortalFormDisplayMode DisplayMode { get; set; }
    }

    public enum PortalFormDisplayMode
    {
        Unknown = 0,
        Edit = 1,
        View = 2,
        Lookup = 3
    }
}
