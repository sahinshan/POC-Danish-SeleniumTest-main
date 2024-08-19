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
    internal class FAQ : IFAQ
    {

        string topTenURL = "api/portal/{0}/faqs/top-ten";
        string categoriesURL = "api/portal/{0}/faqs/categories";
        string categorySeoFaqSeoURL = "api/portal/{0}/faqs/{1}/{2}";
        string categorySeoURL = "api/portal/{0}/faqs/{1}";


        public List<Entities.Portal.FAQ> TopTen(Guid WebsiteID, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(topTenURL, WebsiteID);
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

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entities.Portal.FAQ>>(response.Content);

            return data;
        }

        public List<Entities.Portal.FAQCategory> Categories(Guid WebsiteID, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(categoriesURL, WebsiteID);
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

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entities.Portal.FAQCategory>>(response.Content);

            return data;
        }

        public Entities.Portal.FAQ GetByCategorySEONameAndFAQSEOName(Guid WebsiteID, string CategorySeoName, string FaqSeoName, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(categorySeoFaqSeoURL, WebsiteID, CategorySeoName, FaqSeoName);
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

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.Portal.FAQ>(response.Content);

            return data;
        }

        public List<Entities.Portal.FAQ> GetByCategorySEOName(Guid WebsiteID, string CategorySeoName, string SecurityToken)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("PortalAppURL");

            var client = new RestClient(baseURL);

            string resource = string.Format(categorySeoURL, WebsiteID, CategorySeoName);
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

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entities.Portal.FAQ>>(response.Content);

            return data;
        }

    }
}