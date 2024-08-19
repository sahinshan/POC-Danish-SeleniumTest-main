using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.Portal
{
    public class ExecuteWebsiteHandlerRequest
    {
        public string Handler { get; set; }
        public string JsonData { get; set; }
        public bool Transactional { get; set; }
    }
}
