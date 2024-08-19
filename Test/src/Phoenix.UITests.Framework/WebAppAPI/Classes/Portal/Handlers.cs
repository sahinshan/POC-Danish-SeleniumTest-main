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
    internal class Handlers : IHandlers
    {

        string executeCodeURL = "api/portal/{0}/handlers/code";


        public Entities.Portal.HandlerResponse ExecuteCode(Entities.Portal.ExecuteWebsiteHandlerRequest HandlerRequest, Guid websiteId, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(executeCodeURL, websiteId);
            var request = new RestRequest(resource);


            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(HandlerRequest);

            request.AddJsonBody(jsonBody);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.HandlerResponse>(response.Content);

            return data;

            //return jsonObject.ToString();

            //return Guid.Parse((string)jsonObject["id"]);
        }

    }
}