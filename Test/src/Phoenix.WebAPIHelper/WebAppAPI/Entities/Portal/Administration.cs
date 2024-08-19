using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.Portal
{
    public class Administration
    {
        public string WebsiteUrl { get; set; }
        public bool IsVerificationEmailRequired { get; set; }
        public bool IsUserApprovalRequired { get; set; }
    }
}
