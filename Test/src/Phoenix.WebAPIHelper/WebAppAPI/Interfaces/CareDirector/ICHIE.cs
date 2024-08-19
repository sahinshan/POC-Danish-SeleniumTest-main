using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.WebAPIHelper.WebAppAPI.Interfaces
{
    interface ICHIE
    {
        string Authenticate();

        Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string lastName, DateTime dateOfBirth, string postcode, string SecurityToken);
        Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string lastName, DateTime dateOfBirth, string SecurityToken);
        Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string lastName, string SecurityToken);
        Entities.CareDirector.PersonSearchResult PersonSearch(int top, string firstName, string SecurityToken);
        Entities.CareDirector.PersonSearchResult PersonSearch(int top, string SecurityToken);

        Entities.CareDirector.ChiePersonContactsSearchData GetPersonContacts(Guid PersonID, string access_token);

        Entities.CareDirector.RecordCreationResponse UploadContactAttachment(Entities.CareDirector.ContactAttachmentFileUpload ContactAttachmentFileUpload, string access_token);

        void UpdatePersonRecord(Guid PersonId, Entities.CareDirector.Person person, string access_token);

        Entities.CareDirector.RecordCreationResponse CreateContact(Entities.CareDirector.ContactBO contactRecord, string access_token);
    }
}
