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

            System.Net.Cookie authCookie = new System.Net.Cookie("CareDirectorWebAuth", AuthenticationCookie, "/", domain);
            request.CookieContainer = new System.Net.CookieContainer();
            request.CookieContainer.Add(appUri, authCookie);

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(scheduleJobURL + ScheduleJobID + " Request Failed - " + response.Content);

        }

        
    }
}