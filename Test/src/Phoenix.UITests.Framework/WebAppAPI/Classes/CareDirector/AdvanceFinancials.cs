using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using RestSharp;


namespace Phoenix.UITests.Framework.WebAppAPI.Classes
{
    internal class AdvanceFinancials : IAdvanceFinancials
    {

        string authenticationURL = "api/auth/token";
        string healthCheckURL = "api/health-check";
        string pendingFinanceExtractsURL = "api/core/data/careproviderfinanceextractbatch/system-view/notdownloaded";
        string getSignedURL = "api/files/download-url/careproviderfinanceextractbatch/";
        string setExtractAsDownloadedURL = "api/core/data/careproviderfinanceextractbatch/";


        public string Authenticate()
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");
            string client_id = System.Configuration.ConfigurationManager.AppSettings.Get("client_id");
            string client_secret = System.Configuration.ConfigurationManager.AppSettings.Get("client_secret");

            var client = new RestClient(baseURL);

            var request = new RestRequest(authenticationURL, Method.Post);

            request.AddHeader("cache-control", "no-cache");

            request.AddHeader("content-type", "application/x-www-form-urlencoded");

            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=" + client_id + "&client_secret=" + client_secret, ParameterType.RequestBody);

            RestResponse response = client.Execute(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.StatusCode.ToString() + " - " + response.Content);

            var AdvanceFinancialsAuthData = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.CareDirector.AdvanceFinancialsAuthData>(response.Content);

            return AdvanceFinancialsAuthData.access_token;

        }

        public void HealthCheck(string Access_token)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("IntegrationApiUrl");

            var options = new RestClientOptions(baseURL)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);

            var request = new RestRequest(healthCheckURL, Method.Get);

            request.AddHeader("Authorization", "Bearer " + Access_token);

            var response = client.ExecuteGet(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.StatusCode.ToString() + " - " + response.Content);
        }

        public Entities.CareDirector.AdvanceFinancialsExtractBatchData PendingFinanceExtracts(string Access_token)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("IntegrationApiUrl");

            var options = new RestClientOptions(baseURL)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);

            var request = new RestRequest(pendingFinanceExtractsURL, Method.Get);

            request.AddHeader("Authorization", "Bearer " + Access_token);

            var response = client.ExecuteGet(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.StatusCode.ToString() + " - " + response.Content);

            var financeExtractBatchData = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.CareDirector.AdvanceFinancialsExtractBatchData>(response.Content);

            return financeExtractBatchData;
        }

        public string GetSignedURL(string Access_token, string FinanceExtractBatchContentID)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("IntegrationApiUrl");

            var options = new RestClientOptions(baseURL)
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);

            var request = new RestRequest(getSignedURL + FinanceExtractBatchContentID, Method.Get);
            
            request.AddHeader("Authorization", "Bearer " + Access_token);

            var response = client.ExecuteGet(request);

            if(!response.IsSuccessStatusCode)
                throw new Exception(response.StatusCode.ToString() + " - " + response.Content);

            return response.Content;
        }

        public string DownloadFileFromS3(string Access_token, string DownloadURL)
        {
            DownloadURL = DownloadURL.Replace("\"", "");

            var baseURLStringLenght = DownloadURL.IndexOf(".com/") + 5;
            var baseURL = DownloadURL.Substring(0, baseURLStringLenght);
            var resource = DownloadURL.Substring(baseURLStringLenght, DownloadURL.Length - baseURLStringLenght);

            var options = new RestClientOptions(baseURL)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);

            var request = new RestRequest(resource, Method.Get);
            
            var response = client.ExecuteGet(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.StatusCode.ToString() + " - " + response.Content);

            return response.Content;
        }

        public void SetExtractAsDownloaded(string Access_token, string FinanceExtractBatchID)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("IntegrationApiUrl");

            var options = new RestClientOptions(baseURL)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);

            var request = new RestRequest(setExtractAsDownloadedURL + FinanceExtractBatchID, Method.Put);

            request.AddHeader("Content-Type", "application/json");

            request.AddHeader("Authorization", "Bearer " + Access_token);

            var body = "{ \"isdownloaded\":\"true\" }";

            request.AddStringBody(body, DataFormat.Json);

            RestResponse response = client.ExecutePut(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.StatusCode.ToString() + " - " + response.Content);
        }

    }
}
