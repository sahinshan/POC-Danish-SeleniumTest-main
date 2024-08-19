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
    internal class Data: IData
    {

        string GetPersonDataURL1 = "api/core/data/{0}/{1}";
        string GetPersonDataURL2 = "api/core/data/{0}";


        public T GetBusinessObjectData<T>(string BusinessObjectName, Guid RecordID, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("CaredirectorApiBaseUrl");

            var client = new RestClient(baseURL);

            string resource = string.Format(GetPersonDataURL1, BusinessObjectName, RecordID);
            var request = new RestRequest(resource);


            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");


            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);

            return data;
        }

        public T GetBusinessObjectData<T>(string BusinessObjectName, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("CaredirectorApiBaseUrl");

            var client = new RestClient(baseURL);

            string resource = string.Format(GetPersonDataURL2, BusinessObjectName);
            var request = new RestRequest(resource);


            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");


            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var jsonObject = Newtonsoft.Json.Linq.JObject.Parse(response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content);

            return data;
        }

    }
}