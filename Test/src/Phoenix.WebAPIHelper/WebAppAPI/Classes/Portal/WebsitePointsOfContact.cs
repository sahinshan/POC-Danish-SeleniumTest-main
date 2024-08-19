using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using RestSharp;

namespace Phoenix.WebAPIHelper.WebAppAPI.Classes
{
    internal class WebsitePointsOfContact : IWebsitePointsOfContact
    {

        string contactsURL = "api/portal/{0}/pointsofcontact";

        /// <summary>
        /// api/portal/{0}/contacts
        /// </summary>
        /// <returns></returns>
        public List<Entities.Portal.WebsitePointOfContact> PointsOfContacts(Guid WebsiteID, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(contactsURL, WebsiteID);
            var request = new RestRequest(resource);

            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");
            request.AddParameter("Accept-Encoding", "gzip, deflate, br");

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entities.Portal.WebsitePointOfContact>>(response.Content);

            return data;
        }


    }
}