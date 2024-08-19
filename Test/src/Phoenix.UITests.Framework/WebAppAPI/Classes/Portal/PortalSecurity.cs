using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using RestSharp;

namespace Phoenix.UITests.Framework.WebAppAPI.Classes
{
    internal class PortalSecurity : IPortalSecurity
    {

        string tokenURL = "api/auth/token";

        public string GetToken()
        {
            string baseURL =  System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");
            string ClientId = System.Configuration.ConfigurationManager.AppSettings.Get("ClientId");
            string ClientSecret = System.Configuration.ConfigurationManager.AppSettings.Get("ClientSecret");

            var client = new RestClient(baseURL);

            var request = new RestRequest(tokenURL);

            request.AddParameter("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("Accept", "*/*");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", "");
            request.AddParameter("client_id", ClientId);
            request.AddParameter("client_secret", ClientSecret);
            
            var response = client.Post(request);
            var responseContent = response.Content; //raw content as string

            if (!responseContent.Contains("access_token"))
                throw new Exception(tokenURL + " request failed");

            var jObject = Newtonsoft.Json.Linq.JObject.Parse(responseContent);
            return jObject.GetValue("access_token").ToString();

        }
    }
}
