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
    internal class DataForms : IDataForms
    {

        string dataFormUri1 = "api/portal/{0}/dataforms/{1}";
        string dataFormUri2 = "api/portal/{0}/dataforms/{1}/{2}";
        string dataFormUri3 = "api/portal/{0}/dataforms/{1}/{2}/{3}";
        string dataFormUri4 = "api/portal/{0}/dataforms/get-by-request";
        string saveDataFormUri = "api/portal/{0}/dataforms/save";


        public Entities.Portal.DataForm GetDataForm(Guid websiteId, Guid formId, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(dataFormUri1, websiteId, formId);
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


            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.DataForm>(response.Content);

            return data;
        }

        public Entities.Portal.DataForm GetDataForm(Guid websiteId, Guid formId, Guid id, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(dataFormUri2, websiteId, formId, id);
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


            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.DataForm>(response.Content);

            return data;
        }

        public Entities.Portal.DataForm GetDataForm(Guid websiteId, string businessObjectName, string formName, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(dataFormUri2, websiteId, businessObjectName, formName);
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


            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.DataForm>(response.Content);

            return data;
        }

        public Entities.Portal.DataForm GetDataForm(Guid websiteId, string businessObjectName, string formName, Guid id, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(dataFormUri3, websiteId, businessObjectName, formName, id);
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


            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.DataForm>(response.Content);

            return data;
        }

        public Entities.Portal.DataForm GetDataFormByRequest(Guid websiteId, Entities.Portal.RetrievePortalDataFormRequest dataFormRequest, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(dataFormUri4, websiteId);
            var request = new RestRequest(resource);

            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Content-Type", "application/json");

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(dataFormRequest);

            request.AddJsonBody(jsonBody);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.DataForm>(response.Content);

            return data;
        }

        public string Save(Guid websiteId, Entities.Portal.PortalRecord record, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(saveDataFormUri, websiteId);
            var request = new RestRequest(resource);

            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Content-Type", "application/json");

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(record);

            request.AddJsonBody(jsonBody);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            return response.Content;
        }
    }
}