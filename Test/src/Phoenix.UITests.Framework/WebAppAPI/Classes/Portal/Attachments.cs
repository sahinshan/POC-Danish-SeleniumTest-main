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
    internal class Attachments : IAttachments
    {

        string GetAttachmentInfoURL = "api/portal/{0}/attachment/getattachmentinfo/{1}/{2}";
        string GetURL = "api/portal/{0}/attachment/{1}/{2}/{3}";
        string CheckUploadLimitURL = "api/portal/{0}/attachment/checkuploadlimit";
        string PostURL = "api/portal/{0}/attachment/{1}/{2}/{3}";
        string PutURL1 = "api/portal/{0}/attachment/{1}/{2}";
        string PutURL2 = "api/portal/{0}/attachment/{1}/{2}/{3}/{4}";


        public Entities.Portal.AttachmentsInfoResponse GetAttachmentInfo(Guid websiteId, string type, Guid? id, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource;
            if(id.HasValue)
                resource = string.Format(GetAttachmentInfoURL, websiteId, type, id.Value.ToString());
            else
                resource = string.Format(GetAttachmentInfoURL, websiteId, type, "");
            var request = new RestRequest(resource);


            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.AttachmentsInfoResponse>(response.Content);

            return data;
            //return response.Content;
        }

        public string Get(Guid websiteId, string type, Entities.Portal.FileType fileType, Guid id, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(GetURL, websiteId, type, fileType, id.ToString());
            var request = new RestRequest(resource);


            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);


            return response.Content;
        }

        public string CheckUploadLimit(Guid websiteId, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(CheckUploadLimitURL, websiteId);
            var request = new RestRequest(resource);


            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            
            return response.Content;
        }

        public string Post(Guid websiteId, string type, Guid parentid, string parentfieldname, string FilePath, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(PostURL, websiteId, type, parentid.ToString(), parentfieldname);
            var request = new RestRequest(resource);


            request.AddParameter("Accept", "*/*", ParameterType.HttpHeader);
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            request.AddFile("record", FilePath, "text/plain");


            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            //var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.AttachmentsPostResponse>(response.Content);

            //return data;
            return response.Content;
        }

        public string Put(Guid websiteId, string type, Guid id, string FilePath, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(PutURL1, websiteId, type, id.ToString());
            var request = new RestRequest(resource);


            request.AddParameter("Accept", "*/*", ParameterType.HttpHeader);
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            request.AddFile("record", FilePath, "text/plain");


            var response = client.Put(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Created &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted &&
                response.StatusCode != System.Net.HttpStatusCode.Continue &&
                response.StatusCode != System.Net.HttpStatusCode.Found)
                throw new Exception(response.Content);

            return response.Content;
        }

        public string Put(Guid websiteId, Entities.Portal.FileType fileType, string type, string fileField, Guid id, string FilePath, string SecurityToken, string PortalUserToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(PutURL2, websiteId, fileType, type, fileField, id.ToString());
            var request = new RestRequest(resource);


            request.AddParameter("Accept", "*/*", ParameterType.HttpHeader);
            request.AddParameter("Authorization", "Bearer " + SecurityToken, ParameterType.HttpHeader);

            if (!string.IsNullOrEmpty(PortalUserToken))
                request.AddParameter("PortalUserToken", PortalUserToken, ParameterType.HttpHeader);

            request.AddFile("record", FilePath, "text/plain");

            var response = client.Put(request);

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