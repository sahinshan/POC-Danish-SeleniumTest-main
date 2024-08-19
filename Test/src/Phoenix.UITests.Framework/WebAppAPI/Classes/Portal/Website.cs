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
    internal class Website : IWebsite
    {

        string contactsURL = "api/portal/{0}";


        public Entities.Portal.Website GetWebsiteInfo(Guid WebsiteID, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(contactsURL, WebsiteID);
            var request = new RestRequest(resource);

            request.AddParameter("Accept-Encoding", "application/json");
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");
            request.AddParameter("Accept-Encoding", "gzip, deflate, br");

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(string.IsNullOrEmpty(response.Content) ? response.StatusDescription : response.Content);
            

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.Website>(response.Content);

            return data;
        }

        public Entities.Portal.Website GetWebsiteInfo(string WebsiteID, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(contactsURL, WebsiteID);
            var request = new RestRequest(resource);

            request.AddParameter("Accept-Encoding", "application/json");
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");
            request.AddParameter("Accept-Encoding", "gzip, deflate, br");

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(string.IsNullOrEmpty(response.Content) ? response.StatusDescription : response.Content);


            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.Website>(response.Content);

            return data;
        }


    }
}