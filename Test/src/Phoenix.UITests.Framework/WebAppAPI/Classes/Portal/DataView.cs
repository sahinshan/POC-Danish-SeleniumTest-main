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
    internal class DataView : IDataView
    {

        string GetViewURL = "api/portal/{0}/dataviews";
        string GetLookupViewURL = "api/portal/{0}/dataviews/lookup";


        public Entities.Portal.RetrievePortalDataViewResponse GetView(Guid websiteId, Entities.Portal.RetrievePortalDataViewRequest DataViewRequest, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(GetViewURL, websiteId);
            var request = new RestRequest(resource);


            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(DataViewRequest);

            request.AddJsonBody(jsonBody);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.RetrievePortalDataViewResponse>(response.Content);

            return data;
        }

        public Entities.Portal.RetrievePortalDataViewLookupResponse GetLookupView(Guid websiteId, Entities.Portal.RetrievePortalDataViewRequest DataViewRequest, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(GetLookupViewURL, websiteId);
            var request = new RestRequest(resource);


            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(DataViewRequest);

            request.AddJsonBody(jsonBody);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.RetrievePortalDataViewLookupResponse>(response.Content);

            return data;
        }

    }
}