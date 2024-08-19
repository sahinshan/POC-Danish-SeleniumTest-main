using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Entities.Portal
{
    public class Website
    {
        public Website() 
        {
            Administration = new Administration();
            PasswordComplexity = new PasswordComplexity();
            PasswordPolicy = new PasswordPolicy();
            AccountLockoutPolicy = new AccountLockoutPolicy();
            TwoFactorAuthentication = new TwoFactorAuthentication();
        }


        public Guid PublishedWebsiteId { get; set; }
        public string Name { get; set; }
        public string HomePage { get; set; }
        public string MemberHomePage { get; set; }
        public string Footer { get; set; }
        public string Stylesheet { get; set; }
        public string Script { get; set; }
        public string Logo { get; set; }
        public Guid ApplicationId { get; set; }
        public string UserRecordType { get; set; }
        public Administration Administration { get; set; }
        public PasswordComplexity PasswordComplexity { get; set; }
        public PasswordPolicy PasswordPolicy { get; set; }
        public AccountLockoutPolicy AccountLockoutPolicy { get; set; }
        public TwoFactorAuthentication TwoFactorAuthentication { get; set; }


    }
}
