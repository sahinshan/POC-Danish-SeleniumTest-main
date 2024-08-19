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
    internal class ScheduleJob : IScheduleJob
    {

        string scheduleJobURL = "api/schedulejob/execute/";

        public void Execute(string ScheduleJobID, string AuthenticationCookie)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");
            var appUri = new Uri(baseURL);
            string domain = baseURL.Replace("https://", "").Replace("http://", "").Replace("/", "");

            var client = new RestClient(baseURL);

            var request = new RestRequest(scheduleJobURL + ScheduleJobID);

            //request.AddParameter("CareDirectorWebAuth", AuthenticationCookie, ParameterType.Cookie);
            //client.AddCookie("CareDirectorWebAuth", AuthenticationCookie, "phoenixqa.careworks.ie", request.Resource);
            System.Net.Cookie authCookie = new System.Net.Cookie("CareDirectorWebAuth", AuthenticationCookie, "/", domain);
            request.CookieContainer = new System.Net.CookieContainer();
            request.CookieContainer.Add(appUri, authCookie);

            request.AddHeader("RequestVerificationToken", "0123456789");
            request.AddHeader("User-Agent", "CareDirectorTest");

            //request.AddCookie("__CareDirectorAntiforgeryToken", "0123456789");
            //client.AddCookie("__CareDirectorAntiforgeryToken", "0123456789", "phoenixqa.careworks.ie", request.Resource);
            System.Net.Cookie forgeryTokenCookie = new System.Net.Cookie("__CareDirectorAntiforgeryToken", "0123456789", "/", domain);
            request.CookieContainer.Add(appUri, forgeryTokenCookie);

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(scheduleJobURL + ScheduleJobID + " Request Failed - " + response.Content);

        }

        
    }
}