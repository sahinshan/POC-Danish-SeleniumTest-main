using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.Portal
{
    public class PasswordPolicy
    {
        public int? MaximumPasswordAge { get; set; }
        public int? EnforcePasswordHistory { get; set; }
        public int? MinimumPasswordAge { get; set; }
        public int PasswordResetExpiresIn { get; set; }
    }
}
