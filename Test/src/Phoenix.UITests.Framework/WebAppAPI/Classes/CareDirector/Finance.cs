using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using RestSharp;

namespace Phoenix.UITests.Framework.WebAppAPI.Classes
{
    internal class Finance : IFinance
    {
        
        string readyToAuthorizeInvoiceURL = "api/finance/readytoauthoriseinvoice";

        string authorizeInvoiceURL = "api/finance/authoriseinvoice";

        public void AuthoriseInvoice(Guid InvoiceID, string AuthenticationCookie)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");
            var appUri = new Uri(baseURL);
            string domain = baseURL.Replace("https://", "").Replace("http://", "").Replace("/", "");

            var client = new RestClient(baseURL);

            var request = new RestRequest(authorizeInvoiceURL);

            //request.AddParameter("CareDirectorWebAuth", AuthenticationCookie, ParameterType.Cookie);
            //client.AddCookie("CareDirectorWebAuth", AuthenticationCookie, "phoenixqa.careworks.ie", request.Resource);
            System.Net.Cookie authCookie = new System.Net.Cookie("CareDirectorWebAuth", AuthenticationCookie, "/", domain);
            request.CookieContainer = new System.Net.CookieContainer();
            request.CookieContainer.Add(appUri, authCookie);

            request.AddParameter(
               "application/json",
               "[\"" + InvoiceID + "\"]", // <- ["Guid"]
               ParameterType.RequestBody);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(authorizeInvoiceURL  + "POST Request Failed - " + response.Content);
        }

        public void ReadyToAuthoriseInvoice(Guid InvoiceID, string AuthenticationCookie)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");
            var appUri = new Uri(baseURL);
            string domain = baseURL.Replace("https://", "").Replace("http://", "").Replace("/", "");

            var client = new RestClient(baseURL);

            var request = new RestRequest(readyToAuthorizeInvoiceURL);

            //request.AddParameter("CareDirectorWebAuth", AuthenticationCookie, ParameterType.Cookie);
            //client.AddCookie("CareDirectorWebAuth", AuthenticationCookie, "phoenixqa.careworks.ie", request.Resource);
            System.Net.Cookie authCookie = new System.Net.Cookie("CareDirectorWebAuth", AuthenticationCookie, "/", domain);
            request.CookieContainer = new System.Net.CookieContainer();
            request.CookieContainer.Add(appUri, authCookie);

            request.AddParameter(
               "application/json",
               "[\"" + InvoiceID + "\"]", // <- ["Guid"]
               ParameterType.RequestBody);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(readyToAuthorizeInvoiceURL + "POST Request Failed - " + response.Content);
        }
    }
}