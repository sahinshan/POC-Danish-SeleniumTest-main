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
    internal class WorkflowJob : IWorkflowJob
    {

        string scheduleJobURL = "api/workflow/execute";

        public void Execute(string WorkflowJobID, string AuthenticationCookie)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");
            var appUri = new Uri(baseURL);
            string domain = baseURL.Replace("https://", "").Replace("http://", "").Replace("/", "");

            var client = new RestClient(baseURL);

            var request = new RestRequest(scheduleJobURL);

            System.Net.Cookie authCookie = new System.Net.Cookie("CareDirectorWebAuth", AuthenticationCookie, "/", domain);
            request.CookieContainer = new System.Net.CookieContainer();
            request.CookieContainer.Add(appUri, authCookie);

            request.AddHeader("RequestVerificationToken", "0123456789");
            request.AddHeader("User-Agent", "CareDirectorTest");

            System.Net.Cookie forgeryTokenCookie = new System.Net.Cookie("__CareDirectorAntiforgeryToken", "0123456789", "/", domain);
            request.CookieContainer.Add(appUri, forgeryTokenCookie);

            request.AddJsonBody(new { Id = WorkflowJobID });

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(scheduleJobURL + " Request Failed - " + response.Content);

        }


    }
}