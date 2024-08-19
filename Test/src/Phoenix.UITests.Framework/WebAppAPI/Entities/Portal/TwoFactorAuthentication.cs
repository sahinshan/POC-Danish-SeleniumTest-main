using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.UITests.Framework.WebAppAPI.Entities.Portal
{
    public class TwoFactorAuthentication
    {
        public bool IsEnabled { get; set; }
        public int? PinExpiresIn { get; set; }
        public int? DefaultPinReceivingMethodId { get; set; }
        public int? NumberOfPinDigits { get; set; }
    }
}
