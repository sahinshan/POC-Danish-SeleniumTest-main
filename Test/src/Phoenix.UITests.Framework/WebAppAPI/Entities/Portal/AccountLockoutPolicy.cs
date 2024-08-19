using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.Portal
{
    public class AccountLockoutPolicy
    {
        public bool IsAccountLockingEnabled { get; set; }
        public int? AccountLockoutDuration { get; set; }
        public int? AccountLockoutThreshold { get; set; }
        public int? ResetAccountLockoutCounterAfter { get; set; }
    }
}
