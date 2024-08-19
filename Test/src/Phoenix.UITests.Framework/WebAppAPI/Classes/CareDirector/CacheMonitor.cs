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
    internal class CacheMonitor : ICacheMonitor
    {

        string scheduleJobURL = "admin/cachepage.aspx";

        public void Execute(string CWCacheKey, string CWServerName, string CWCacheItemName, string AuthenticationCookie)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");
            var appUri = new Uri(baseURL);
            string domain = baseURL.Replace("https://", "").Replace("http://", "").Replace("/", "");

            var client = new RestClient(baseURL);

            var request = new RestRequest(scheduleJobURL);

            //request.AddParameter("CareDirectorWebAuth", AuthenticationCookie, ParameterType.Cookie);
            //client.AddCookie("CareDirectorWebAuth", AuthenticationCookie, "phoenixqa.careworks.ie", request.Resource);
            System.Net.Cookie authCookie = new System.Net.Cookie("CareDirectorWebAuth", AuthenticationCookie, "/", domain);
            request.CookieContainer = new System.Net.CookieContainer();
            request.CookieContainer.Add(appUri, authCookie);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("__EVENTTARGET=RecycleCache&__EVENTARGUMENT=&__VIEWSTATE=&__VIEWSTATEGENERATOR=&__EVENTVALIDATION=&CWCacheKey={0}&CWServerName={1}&CWCacheItemName={2}", CWCacheKey, CWServerName, CWCacheItemName);
            request.AddJsonBody(sb.ToString());

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(scheduleJobURL + " Request Failed - " + response.Content);

        }


    }
}