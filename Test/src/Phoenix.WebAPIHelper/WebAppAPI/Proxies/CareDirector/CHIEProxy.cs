using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Phoenix.WebAPIHelper.WebAppAPI.Interfaces;
using Phoenix.WebAPIHelper.WebAppAPI.Classes;

namespace Phoenix.WebAPIHelper.WebAppAPI.Proxies
{
    public class CHIEProxy : ICHIE
    {
        private ICHIE _chieClass;

        public string access_token { get; set; }

        public CHIEProxy()
        {
            _chieClass = new CHIE();
        }

        public string Authenticate()
        {
            access_token = _chieClass.Authenticate();
            return access_token;
        }

        public Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string lastName, DateTime dateOfBirth, string postcode, string access_token)
        {
            return _chieClass.PersonSearch(top, firstName, lastName, dateOfBirth, postcode, access_token);
        }

        public Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string lastName, DateTime dateOfBirth, string access_token)
        {
            return _chieClass.PersonSearch(top, firstName, lastName, dateOfBirth, access_token);
        }

        public Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string lastName, string access_token)
        {
            return _chieClass.PersonSearch(top, firstName, lastName, access_token);
        }

        public Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string access_token)
        {
            return _chieClass.PersonSearch(top, firstName, access_token);
        }

        public Entities.CareDirector.PersonSearchResult PersonSearch(int top, string access_token)
        {
            return _chieClass.PersonSearch(top, access_token);
        }

        public Entities.CareDirector.ChiePersonContactsSearchData GetPersonContacts(Guid PersonID, string access_token)
        {
            return _chieClass.GetPersonContacts(PersonID, access_token);
        }

        public Entities.CareDirector.RecordCreationResponse UploadContactAttachment(Entities.CareDirector.ContactAttachmentFileUpload ContactAttachmentFileUpload, string access_token)
        {
            return _chieClass.UploadContactAttachment(ContactAttachmentFileUpload, access_token);
        }

        public void UpdatePersonRecord(Guid PersonId, Entities.CareDirector.Person person, string access_token)
        {
            _chieClass.UpdatePersonRecord(PersonId, person, access_token);
        }

        public Entities.CareDirector.RecordCreationResponse CreateContact(Entities.CareDirector.ContactBO contactRecord, string access_token)
        {
            return _chieClass.CreateContact(contactRecord, access_token);
        }
    }
}
