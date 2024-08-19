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
    internal class Audit : IAudit
    {

        string auditSearchURL = "api/audit/RetrieveAudits";

        public Entities.CareDirector.AuditResponse RetrieveAudits(Phoenix.UITests.Framework.WebAppAPI.Entities.CareDirector.AuditSearch AuditSearch, string AuthenticationCookie)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");
            var appUri = new Uri(baseURL);
            string domain = baseURL.Replace("https://", "").Replace("http://", "").Replace("/", "");

            var client = new RestClient(baseURL);

            var request = new RestRequest(auditSearchURL);

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

            request.AddJsonBody(AuditSearch);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(auditSearchURL + " Request Failed - " + response.Content);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.CareDirector.AuditResponse>(response.Content);

        }

        
    }
}