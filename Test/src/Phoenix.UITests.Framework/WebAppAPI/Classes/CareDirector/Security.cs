using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.UITests.Framework.WebAppAPI.Interfaces;
using RestSharp;


namespace Phoenix.UITests.Framework.WebAppAPI.Classes
{
    internal class Security : ISecurity
    {

        string authenticationURL = "api/security/authenticateuser";

        /// <summary>
        /// Login using the Web App API
        /// </summary>
        /// <returns>The .CareDirectorWebAuth Cookie that must be supplied in each request to the web API</returns>
        public string Authenticate()
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");
            var appUri = new Uri(baseURL);
            string domain = baseURL.Replace("https://", "").Replace("http://", "").Replace("/", "");
            string TenantName = System.Configuration.ConfigurationManager.AppSettings.Get("TenantName");
            string Username = System.Configuration.ConfigurationManager.AppSettings.Get("Username");
            string Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password");
            string RememberDetails = System.Configuration.ConfigurationManager.AppSettings.Get("RememberDetails");
            string DataEncoded = System.Configuration.ConfigurationManager.AppSettings.Get("DataEncoded");

            if (DataEncoded.Equals("true"))
            {
                var base64EncodedBytes = System.Convert.FromBase64String(Username);
                Username = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

                base64EncodedBytes = System.Convert.FromBase64String(Password);
                Password = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }

            var client = new RestClient(baseURL);

            var request = new RestRequest(authenticationURL);

            request.AddParameter("TenantName", TenantName);
            request.AddParameter("Username", Username);
            request.AddParameter("Password", Password);
            request.AddParameter("RememberDetails", RememberDetails);

            request.AddHeader("RequestVerificationToken", "0123456789");
            request.AddHeader("User-Agent", "CareDirectorTest");
            //request.AddCookie("__CareDirectorAntiforgeryToken", "0123456789");
            System.Net.Cookie newCookie = new System.Net.Cookie("__CareDirectorAntiforgeryToken", "0123456789", "/", domain);
            request.CookieContainer = new System.Net.CookieContainer();
            request.CookieContainer.Add(appUri, newCookie);



            var response = client.Post(request);
            var responseContent = response.Content; //raw content as string

            if (!responseContent.Contains("\"Success\":true"))
                throw new Exception(authenticationURL + " request failed");

            return request.CookieContainer.GetCookies(appUri)["CareDirectorWebAuth"].Value;
            //return response.Cookies.Where(c => c.Name == "CareDirectorWebAuth").FirstOrDefault().Value;
        }

        public string Authenticate(string TenantName, string UserName, string Password)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");
            var appUri = new Uri(baseURL);
            string domain = baseURL.Replace("https://", "").Replace("http://", "").Replace("/", "");
            string RememberDetails = System.Configuration.ConfigurationManager.AppSettings.Get("RememberDetails");

            var client = new RestClient(baseURL);

            var request = new RestRequest(authenticationURL);

            request.AddParameter("TenantName", TenantName);
            request.AddParameter("Username", UserName);
            request.AddParameter("Password", Password);
            request.AddParameter("RememberDetails", RememberDetails);

            request.AddHeader("RequestVerificationToken", "0123456789");
            request.AddHeader("User-Agent", "CareDirectorTest");
            //request.AddCookie("__CareDirectorAntiforgeryToken", "0123456789");
            //client.AddCookie("__CareDirectorAntiforgeryToken", "0123456789", "phoenixqa.careworks.ie", request.Resource);
            System.Net.Cookie newCookie = new System.Net.Cookie("__CareDirectorAntiforgeryToken", "0123456789", "/", domain);
            request.CookieContainer = new System.Net.CookieContainer();
            request.CookieContainer.Add(appUri, newCookie);


            var response = client.Post(request);
            var responseContent = response.Content; //raw content as string

            if (!responseContent.Contains("\"Success\":true"))
                throw new Exception(authenticationURL + " request failed");

            return request.CookieContainer.GetCookies(appUri)["CareDirectorWebAuth"].Value;
            //return response.Cookies.Where(c => c.Name == "CareDirectorWebAuth").FirstOrDefault().Value;
        }
    }
}
