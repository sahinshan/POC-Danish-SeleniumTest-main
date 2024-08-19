using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.Portal
{
    public class PasswordComplexity
    {
        public int MinimumPasswordLength { get; set; }
        public int? MinimumSpecialCharacters { get; set; }
        public int? MinimumNumericCharacters { get; set; }
        public int? MinimumUppercaseLetters { get; set; }
    }
}
