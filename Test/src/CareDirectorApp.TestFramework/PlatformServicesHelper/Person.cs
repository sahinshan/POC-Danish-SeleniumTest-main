using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class Person : BaseClass
    {

        public Person(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Dictionary<string, object> GetPersonByName(string FirstName, string MiddleName, string LastName, params string[] fields)
        {
            var query = new DataQuery("Person", true, fields);
            query.PrimaryKeyName = "PersonId";

            if (!string.IsNullOrEmpty(FirstName))
                query.Filter.AddCondition("Person", "FirstName", ConditionOperatorType.Equal, FirstName);
            if (!string.IsNullOrEmpty(MiddleName))
                query.Filter.AddCondition("Person", "MiddleName", ConditionOperatorType.Equal, MiddleName);
            if (!string.IsNullOrEmpty(LastName))
                query.Filter.AddCondition("Person", "LastName", ConditionOperatorType.Equal, LastName);

            var response = DataProvider.ExecuteDataQuery(query);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            if (response.BusinessDataCollection.Count > 0)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                (from fc in response.BusinessDataCollection[0].FieldCollection
                 select new
                 {
                     key = fc.Key,
                     value = fc.Value
                 })
                 .ToList()
                 .ForEach(c =>
                 {
                     dic.Add(c.key, c.value);
                 });

                return dic;
            }
            else
                return new Dictionary<string, object>();
        }

        public void DeletePerson(Guid PersonID)
        {
            var response = DataProvider.Delete("Person", PersonID);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

        }

        public Guid CreatePersonRecord(
            string Title, string FirstName, string MiddleName, string LastName, string PreferredName,
            DateTime DateOfBirth, Guid Ethnicity, Guid MaritalStatus, Guid LanguageId, Guid AddressPropertyTypeId, Guid OwnerID,
            string PropertyName, string AddressLine1, string Country, string AddressLine2, string AddressLine3, string UPRN, string AddressLine4, string AddressLine5, string Postcode,
            string NHSNumber, string BusinessPhone, string Telephone2, string Telephone3, string Telephone1, string HomePhone, string MobilePhone, string PrimaryEmail, string SecondaryEmail,
            int AddressTypeId, int AccommodationStatusId, int LivesAloneTypeId, int GenderId)
        {
            var dataObject = GetBusinessDataBaseObject("Person", "PersonId");

            dataObject.FieldCollection.Add("Title", Title);
            dataObject.FieldCollection.Add("FirstName", FirstName);
            dataObject.FieldCollection.Add("MiddleName", MiddleName);
            dataObject.FieldCollection.Add("LastName", LastName);
            dataObject.FieldCollection.Add("PreferredName", PreferredName);

            dataObject.FieldCollection.Add("DateOfBirth", DateOfBirth);
            dataObject.FieldCollection.Add("EthnicityId", Ethnicity);
            dataObject.FieldCollection.Add("MaritalStatusId", MaritalStatus);
            dataObject.FieldCollection.Add("LanguageId", LanguageId);
            dataObject.FieldCollection.Add("AddressPropertyTypeId", AddressPropertyTypeId);
            dataObject.FieldCollection.Add("OwnerID", OwnerID);

            string fullAddress = string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", PropertyName, AddressLine1, AddressLine2, AddressLine3, AddressLine4, AddressLine5, Postcode);
            dataObject.FieldCollection.Add("FullAddress", fullAddress);

            dataObject.FieldCollection.Add("PropertyName", PropertyName);
            dataObject.FieldCollection.Add("AddressLine1", AddressLine1);
            dataObject.FieldCollection.Add("Country", Country);
            dataObject.FieldCollection.Add("AddressLine2", AddressLine2);
            dataObject.FieldCollection.Add("AddressLine3", AddressLine3);
            dataObject.FieldCollection.Add("UPRN", UPRN);
            dataObject.FieldCollection.Add("AddressLine4", AddressLine4);
            dataObject.FieldCollection.Add("AddressLine5", AddressLine5);
            dataObject.FieldCollection.Add("Postcode", Postcode);

            dataObject.FieldCollection.Add("NHSNumber", NHSNumber);
            dataObject.FieldCollection.Add("BusinessPhone", BusinessPhone);
            dataObject.FieldCollection.Add("Telephone2", Telephone2);
            dataObject.FieldCollection.Add("Telephone3", Telephone3);
            dataObject.FieldCollection.Add("Telephone1", Telephone1);
            dataObject.FieldCollection.Add("HomePhone", HomePhone);
            dataObject.FieldCollection.Add("MobilePhone", MobilePhone);
            dataObject.FieldCollection.Add("PrimaryEmail", PrimaryEmail);
            dataObject.FieldCollection.Add("SecondaryEmail", SecondaryEmail);

            dataObject.FieldCollection.Add("AddressTypeId", AddressTypeId);
            dataObject.FieldCollection.Add("AccommodationStatusId", AccommodationStatusId);
            dataObject.FieldCollection.Add("LivesAloneTypeId", LivesAloneTypeId);
            dataObject.FieldCollection.Add("GenderId", GenderId);

            dataObject.FieldCollection.Add("GenderId", GenderId);

            dataObject.FieldCollection.Add("AllergiesNotRecorded", true);
            dataObject.FieldCollection.Add("Deceased", false);
            dataObject.FieldCollection.Add("Inactive", false);
            dataObject.FieldCollection.Add("RetainInformationConcern", false);
            dataObject.FieldCollection.Add("AllowEmail", false);
            dataObject.FieldCollection.Add("AllowMail", false);
            dataObject.FieldCollection.Add("AllowPhone", false);
            dataObject.FieldCollection.Add("IsExternalPerson", false);
            dataObject.FieldCollection.Add("SuppressStatementInvoices", false);
            dataObject.FieldCollection.Add("RepresentAlertOrHazard", false);
            dataObject.FieldCollection.Add("KnownAllergies", false);
            dataObject.FieldCollection.Add("NoKnownAllergies", false);
            dataObject.FieldCollection.Add("InterpreterRequired", false);
            dataObject.FieldCollection.Add("ChildProtectionFlag", false);
            dataObject.FieldCollection.Add("RelatedChildProtectionFlag", false);
            dataObject.FieldCollection.Add("AdultSafeguardingFlag", false);
            dataObject.FieldCollection.Add("RelatedAdultSafeguardingFlag", false);
            dataObject.FieldCollection.Add("RecordedInError", false);


            CareDirector.Sdk.ServiceResponse.CreateResponse response = DataProvider.Create(dataObject);

            if (response.HasErrors)
                throw new Exception(response.Exception.Message);

            return response.Id.Value;
        }

        public void UpdateLinkedAddress(Guid PersonID, Guid? LinkedAddressId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("Person", "PersonId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PersonID", PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "LinkedAddressId", LinkedAddressId);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}
