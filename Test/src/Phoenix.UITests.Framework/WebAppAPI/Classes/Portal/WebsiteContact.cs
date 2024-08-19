using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using RestSharp;

namespace Phoenix.UITests.Framework.WebAppAPI.Classes
{
    internal class WebsiteContact : IWebsiteContact
    {

        string contactsURL = "api/portal/{0}/contact";

        
        public Guid CreateContact(Entities.Portal.WebsiteContact WebsiteContact, Guid WebsiteID, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(contactsURL, WebsiteID);
            var request = new RestRequest(resource);

            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Content-Type", "application/json");

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(WebsiteContact);

            request.AddJsonBody(jsonBody);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK && 
                response.StatusCode != System.Net.HttpStatusCode.Created && 
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(response.Content);

            return Guid.Parse((string)jsonObject["id"]);
        }


    }
}