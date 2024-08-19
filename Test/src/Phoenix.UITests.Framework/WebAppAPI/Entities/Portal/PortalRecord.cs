using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.Portal
{
    public class PortalRecord
    {
        public PortalRecord()
        {
            Fields = new Dictionary<string, string>();
        }

        public Guid? Id { get; set; }
        public string BusinessObjectName { get; set; }
        public Dictionary<string, string> Fields { get; set; }
    }
}
