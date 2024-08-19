using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
{
    public class SecurityProxy : ISecurity
    {
        public SecurityProxy()
        {
            _securityClass = new Security();
        }


        private ISecurity _securityClass;

        /// <summary>
        /// After the login request this property will hold the authentication cookie value (CareDirectorWebAuth).
        /// This cookie is necessary in posterior requests to the CareDirector v6 rest API
        /// </summary>
        public string AuthenticationCookie { get; set; }


        /// <summary>
        /// Login using the Web App API
        /// </summary>
        /// <returns>The CareDirectorWebAuth Cookie that must be supplied in each request to the web API</returns>
        public string Authenticate()
        {
            this.AuthenticationCookie = _securityClass.Authenticate();

            return this.AuthenticationCookie;
        }

        public string Authenticate(string EnvironmentID, string UserName, string Password)
        {
            this.AuthenticationCookie = _securityClass.Authenticate(EnvironmentID, UserName, Password);
            
            return this.AuthenticationCookie;
        }
    }
}
