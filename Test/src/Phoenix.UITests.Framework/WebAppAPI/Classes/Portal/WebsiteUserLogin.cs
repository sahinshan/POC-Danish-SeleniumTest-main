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
    internal class WebsiteUserLogin : IWebsiteUserLogin
    {

        string userLoginURL = "api/portal/{0}/users/login";
        string validatePinURL = "api/portal/{0}/users/validatepin";
        string reissuePinURL = "api/portal/{0}/users/reissuepin";


        public Entities.Portal.WebsiteUserAccessData LoginUser(Guid WebsiteID, Entities.Portal.WebsiteUserLogin UserLoginInfo, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(userLoginURL, WebsiteID);
            var request = new RestRequest(resource);

            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Content-Type", "application/json");

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(UserLoginInfo);

            request.AddJsonBody(jsonBody);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.WebsiteUserAccessData>(response.Content);

            return data;
        }

        public Entities.Portal.WebsiteUserAccessData ValidatePin(Guid WebsiteID, Entities.Portal.WebsiteUserPin UserPinInfo, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(validatePinURL, WebsiteID);
            var request = new RestRequest(resource);

            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Content-Type", "application/json");

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(UserPinInfo);

            request.AddJsonBody(jsonBody);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.WebsiteUserAccessData>(response.Content);

            return data;
        }

        public Entities.Portal.WebsiteUserAccessData ReissuePin(Guid WebsiteID, Entities.Portal.WebsiteUserReIssuePin reissuePin, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(reissuePinURL, WebsiteID);
            var request = new RestRequest(resource);

            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Content-Type", "application/json");

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(reissuePin);

            request.AddJsonBody(jsonBody);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.WebsiteUserAccessData>(response.Content);

            return data;
        }

        //
    }
}