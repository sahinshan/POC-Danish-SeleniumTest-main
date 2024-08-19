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
    internal class WebsitePages : IWebsitePages
    {

        string pagesURL = "api/portal/{0}/pages";
        string pageURL = "api/portal/{0}/pages/{1}";


        public List<Entities.Portal.WebsitePage> GetWebsitePagesInfo(Guid WebsiteID, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(pagesURL, WebsiteID);
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


            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entities.Portal.WebsitePage>>(response.Content);

            return data;
        }

        public Entities.Portal.WebsitePage GetWebsitePage(Guid WebsiteID, string PageName, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(pageURL, WebsiteID, PageName);
            var request = new RestRequest(resource);

            request.AddParameter("Accept-Encoding", "application/json");
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");
            request.AddParameter("Accept-Encoding", "gzip, deflate, br");

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            
            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(string.IsNullOrEmpty(response.Content) ? response.StatusDescription : response.Content);


            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.WebsitePage>(response.Content);

            return data;
        }


    }
}