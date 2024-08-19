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
    internal class CHIE : ICHIE
    {

        string authenticationURL = "services/api/auth/token";

        string personSearchURL_Top_FN_LN_DOB_PC = "Services/api/core/data/person/system-view/cw-chie?$top={0}&$filter=firstname eq '{1}' and lastname eq '{2}' and dateofbirth eq '{3}' and postcode eq '{4}'";
        string personSearchURL_Top_FN_LN_DOB = "Services/api/core/data/person/system-view/cw-chie?$top={0}&$filter=firstname eq '{1}' and lastname eq '{2}' and dateofbirth eq '{3}'";
        string personSearchURL_Top_FN_LN = "Services/api/core/data/person/system-view/cw-chie?$top={0}&$filter=firstname eq '{1}' and lastname eq '{2}'";
        string personSearchURL_Top_FN = "Services/api/core/data/person/system-view/cw-chie?$top={0}&$filter=firstname eq '{1}'";
        string personSearchURL_Top = "Services/api/core/data/person/system-view/cw-chie?$top={0}";

        string GetPersonContactsURL = "Services/api/core/data/person/{0}/contacts/system-view/cw-chie";

        string uploadContactAttachmentURL = "services/api/files/contactattachment";

        string updatePersonURL = "services/api/core/data/person/{0}";

        string createContact = "services/api/core/data/contact";


        public string Authenticate()
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");

            string client_id = System.Configuration.ConfigurationManager.AppSettings.Get("client_id");
            string client_secret = System.Configuration.ConfigurationManager.AppSettings.Get("client_secret");


            var client = new RestClient(baseURL);

            var request = new RestRequest(authenticationURL);

            request.AddParameter("client_id", client_id);
            request.AddParameter("client_secret", client_secret);

            var response = client.Post(request);
            var responseContent = response.Content; //raw content as string

            if (!responseContent.Contains("access_token"))
                throw new Exception(baseURL + " request failed --> " + response.Content);

            var jObject = Newtonsoft.Json.Linq.JObject.Parse(responseContent);
            return jObject.GetValue("access_token").ToString();
        }

        public Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string lastName, DateTime dateOfBirth, string postcode, string access_token)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");

            var client = new RestClient(baseURL);

            var DateOfBirthStringFormat = dateOfBirth.ToString("yyyy-MM-dd");
            var request = new RestRequest(string.Format(personSearchURL_Top_FN_LN_DOB_PC, top, firstName, lastName, DateOfBirthStringFormat, postcode));

            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + access_token, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(personSearchURL_Top_FN_LN_DOB_PC + " Request Failed - " + response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.CareDirector.PersonSearchResult>(response.Content);

            return data;
        }

        public Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string lastName, DateTime dateOfBirth, string access_token)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");

            var client = new RestClient(baseURL);

            var DateOfBirthStringFormat = dateOfBirth.ToString("yyyy-MM-dd");
            var request = new RestRequest(string.Format(personSearchURL_Top_FN_LN_DOB, top, firstName, lastName, DateOfBirthStringFormat));

            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + access_token, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(personSearchURL_Top_FN_LN_DOB + " Request Failed - " + response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.CareDirector.PersonSearchResult>(response.Content);

            return data;

        }

        public Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string lastName, string access_token)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");

            var client = new RestClient(baseURL);

            var request = new RestRequest(string.Format(personSearchURL_Top_FN_LN, top, firstName, lastName));

            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + access_token, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(personSearchURL_Top_FN_LN + " Request Failed - " + response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.CareDirector.PersonSearchResult>(response.Content);

            return data;

        }

        public Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string access_token)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");

            var client = new RestClient(baseURL);

            var request = new RestRequest(string.Format(personSearchURL_Top_FN, top, firstName));

            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + access_token, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(personSearchURL_Top_FN + " Request Failed - " + response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.CareDirector.PersonSearchResult>(response.Content);

            return data;

        }

        public Entities.CareDirector.PersonSearchResult PersonSearch(int top, string access_token)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");

            var client = new RestClient(baseURL);

            var request = new RestRequest(string.Format(personSearchURL_Top, top));

            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + access_token, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(personSearchURL_Top + " Request Failed - " + response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.CareDirector.PersonSearchResult>(response.Content);

            return data;
        }

        public Entities.CareDirector.ChiePersonContactsSearchData GetPersonContacts(Guid PersonID, string access_token)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");

            var client = new RestClient(baseURL);

            var request = new RestRequest(string.Format(GetPersonContactsURL, PersonID.ToString()));

            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + access_token, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            var response = client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(GetPersonContactsURL + " Request Failed - " + response.Content);

            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.CareDirector.ChiePersonContactsSearchData>(response.Content);

            return data;
        }

        public Entities.CareDirector.RecordCreationResponse UploadContactAttachment(Entities.CareDirector.ContactAttachmentFileUpload ContactAttachmentFileUpload, string access_token)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");

            var client = new RestClient(baseURL);

            var request = new RestRequest(uploadContactAttachmentURL);
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + access_token, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            request.AddJsonBody(ContactAttachmentFileUpload);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.Created)
                throw new Exception(GetPersonContactsURL + " Request Failed - " + response.Content);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.CareDirector.RecordCreationResponse>(response.Content);
        }

        public void UpdatePersonRecord(Guid PersonId, Entities.CareDirector.Person person, string access_token)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");

            var client = new RestClient(baseURL);

            var request = new RestRequest(string.Format(updatePersonURL, PersonId));
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + access_token, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            request.AddJsonBody(person);

            var response = client.Put(request);

            if (response.StatusCode != System.Net.HttpStatusCode.Created)
                throw new Exception(GetPersonContactsURL + " Request Failed - " + response.Content);

            return;
        }

        public Entities.CareDirector.RecordCreationResponse CreateContact(Entities.CareDirector.ContactBO contactRecord, string access_token)
        {
            string baseURL = System.Configuration.ConfigurationManager.AppSettings.Get("appURL");

            var client = new RestClient(baseURL);

            var request = new RestRequest(createContact);
            request.RequestFormat = DataFormat.Json;

            request.AddParameter("Accept-Encoding", "gzip, deflate, br");
            request.AddParameter("Content-Type", "application/json");
            request.AddParameter("Authorization", "Bearer " + access_token, ParameterType.HttpHeader);
            request.AddParameter("Accept", "*/*");

            request.AddJsonBody(contactRecord);

            var response = client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.Created)
                throw new Exception(GetPersonContactsURL + " Request Failed - " + response.Content);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Entities.CareDirector.RecordCreationResponse>(response.Content);
        }

    }
}