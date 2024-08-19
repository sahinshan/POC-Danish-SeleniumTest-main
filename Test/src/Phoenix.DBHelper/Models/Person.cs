using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phoenix.DBHelper.Models
{
    public class Person : BaseClass
    {

        private string tableName = "person";
        private string primaryKeyName = "personid";

        public Person()
        {
            AuthenticateUser();
        }

        public Person(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Dictionary<string, object> GetPersonByName(string FirstName, string MiddleName, string LastName, params string[] fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            if (!string.IsNullOrEmpty(FirstName))
                this.BaseClassAddTableCondition(query, "FirstName", ConditionOperatorType.Equal, FirstName);

            if (!string.IsNullOrEmpty(MiddleName))
                this.BaseClassAddTableCondition(query, "MiddleName", ConditionOperatorType.Equal, MiddleName);

            if (!string.IsNullOrEmpty(LastName))
                this.BaseClassAddTableCondition(query, "LastName", ConditionOperatorType.Equal, LastName);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByPreferredName(string PreferredName)
        {
            return GetByPreferredName(PreferredName, 50000);
        }

        public List<Guid> GetByPreferredName(string PreferredName, int recordsPerPage)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PreferredName", ConditionOperatorType.Like, PreferredName);

            this.AddReturnField(query, tableName, primaryKeyName);

            query.RecordsPerPage = recordsPerPage;

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByPreferredName(string PreferredName, Guid OwnerId, int recordsPerPage)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PreferredName", ConditionOperatorType.Like, PreferredName);
            this.BaseClassAddTableCondition(query, "OwnerId", ConditionOperatorType.Equal, OwnerId);

            this.AddReturnField(query, tableName, primaryKeyName);

            query.RecordsPerPage = recordsPerPage;

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByResponsibleTeam(Guid OwnerId, int recordsPerPage)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "OwnerId", ConditionOperatorType.Equal, OwnerId);

            this.AddReturnField(query, tableName, primaryKeyName);

            query.RecordsPerPage = recordsPerPage;

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByFirstName(string FirstName)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "FirstName", ConditionOperatorType.Equal, FirstName);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByFirstAndLastName(string FirstName, string LastName)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "FirstName", ConditionOperatorType.Equal, FirstName);
            this.BaseClassAddTableCondition(query, "LastName", ConditionOperatorType.Equal, LastName);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByPrimaryEmail(string PrimaryEmail)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PrimaryEmail", ConditionOperatorType.Equal, PrimaryEmail);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByFullName(string FullName)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "FullName", ConditionOperatorType.Equal, FullName);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetPersonById(Guid PersonID, params string[] fields)
        {
            System.Threading.Thread.Sleep(1000);
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.AddReturnFields(query, tableName, fields);
            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePerson(Guid PersonID)
        {
            this.DeleteRecord(tableName, PersonID);
        }

        /// <summary>
        /// This method will create multiple person records, generating random first and last names
        /// </summary>
        /// <param name="InitializationSeed"></param>
        /// <param name="Titles"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="DateOfBirth"></param>
        /// <param name="Ethnicity"></param>
        /// <param name="OwnerID"></param>
        /// <param name="AddressTypeId"></param>
        /// <param name="GenderId"></param>
        /// <returns></returns>
        public List<Guid> CreateMultiplePersonRecords(int TotalRecordsToCreate, List<string> Titles, List<string> FirstNames, List<string> LastNames,
            DateTime MinDateOfBirth, DateTime MaxDateOfBirth, List<Guid> Ethnicities, Guid OwnerID, int AddressTypeId, List<int> Genders)
        {
            var allRecordsToCreate = new List<BusinessData>();

            //we cannot insert more than 1000 records at once
            if (TotalRecordsToCreate > 1000)
                TotalRecordsToCreate = 1000;

            var rnd = new Random();
            var totalTitles = Titles.Count;
            var totalFirstNames = FirstNames.Count;
            var totalLastNames = LastNames.Count;
            var datesOfBirthDifferenceInDays = (MaxDateOfBirth - MinDateOfBirth).Days;
            var totalEthnicities = Ethnicities.Count;
            var totlaGenders = Genders.Count;


            for (int i = 0; i < TotalRecordsToCreate; i++)
            {
                var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

                var title = Titles[rnd.Next(0, totalTitles)];
                var firstName = FirstNames[rnd.Next(0, totalFirstNames)];
                var lastName = LastNames[rnd.Next(0, totalLastNames)];
                var DateOfBirth = MinDateOfBirth.AddDays(rnd.Next(0, datesOfBirthDifferenceInDays));
                var ethnicityId = Ethnicities[rnd.Next(0, totalEthnicities)];
                var genderId = Genders[rnd.Next(0, totlaGenders)];

                dataObject.FieldCollection.Add("Title", title);
                dataObject.FieldCollection.Add("FirstName", firstName);
                dataObject.FieldCollection.Add("MiddleName", "LoadDataScript " + firstName);
                dataObject.FieldCollection.Add("LastName", lastName);
                dataObject.FieldCollection.Add("PreferredName", "LoadDataScript " + lastName);

                dataObject.FieldCollection.Add("DateOfBirth", DateOfBirth);
                dataObject.FieldCollection.Add("EthnicityId", ethnicityId);
                dataObject.FieldCollection.Add("OwnerID", OwnerID);

                dataObject.FieldCollection.Add("AddressTypeId", AddressTypeId);
                dataObject.FieldCollection.Add("GenderId", genderId);

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

                allRecordsToCreate.Add(dataObject);
            }

            return this.CreateMultipleRecords(allRecordsToCreate);
        }

        public Guid CreatePersonRecord(string Title, string FirstName, string MiddleName, string LastName, string PreferredName, DateTime DateOfBirth, Guid Ethnicity, Guid OwnerID, int AddressTypeId, int GenderId)
        {
            return CreatePersonRecord(Title, FirstName, MiddleName, LastName, PreferredName, DateOfBirth, Ethnicity, OwnerID, AddressTypeId, GenderId, null);
        }

        public Guid CreatePersonRecord(Guid PersonId, string Title, string FirstName, string MiddleName, string LastName, string PreferredName, DateTime DateOfBirth, Guid Ethnicity, Guid OwnerID, int AddressTypeId, int GenderId)
        {
            return CreatePersonRecord(PersonId, Title, FirstName, MiddleName, LastName, PreferredName, DateOfBirth, Ethnicity, OwnerID, AddressTypeId, GenderId, null, null, null, null);
        }

        public Guid CreatePersonRecord(string Title, string FirstName, string MiddleName, string LastName, string PreferredName, DateTime DateOfBirth, Guid Ethnicity, Guid OwnerID, int AddressTypeId, int GenderId, string NHSNumber)
        {
            return CreatePersonRecord(Title, FirstName, MiddleName, LastName, PreferredName, DateOfBirth, Ethnicity, OwnerID, AddressTypeId, GenderId, NHSNumber, null, null, null);
        }

        public Guid CreatePersonRecord(string Title, string FirstName, string MiddleName, string LastName, string PreferredName, DateTime DateOfBirth,
            Guid Ethnicity, Guid OwnerID, int AddressTypeId, int GenderId, string NHSNumber, string LegacyId, string NationalInsuranceNumber, string UniquePupilNumber,
            string PropertyName = "", string PropertyNo = "", string Street = "", string VlgDistrict = "", string TownCity = "", string County = "", string Postcode = "")
        {
            return CreatePersonRecord(Title, FirstName, MiddleName, LastName, PreferredName, DateOfBirth,
            Ethnicity, OwnerID, null, AddressTypeId, GenderId, NHSNumber, LegacyId, NationalInsuranceNumber, UniquePupilNumber,
            PropertyName, PropertyNo, Street, VlgDistrict, TownCity, County, Postcode);
        }

        public Guid CreatePersonRecord(string Title, string FirstName, string MiddleName, string LastName, string PreferredName, DateTime? DateOfBirth,
           Guid Ethnicity, Guid OwnerID, DateTime? addressstartdate, int AddressTypeId, int GenderId, string NHSNumber, string LegacyId, string NationalInsuranceNumber, string UniquePupilNumber,
           string PropertyName = "", string PropertyNo = "", string Street = "", string VlgDistrict = "", string TownCity = "", string County = "", string Postcode = "")
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            dataObject.FieldCollection.Add("Title", Title);
            dataObject.FieldCollection.Add("FirstName", FirstName);
            dataObject.FieldCollection.Add("MiddleName", MiddleName);
            dataObject.FieldCollection.Add("LastName", LastName);
            dataObject.FieldCollection.Add("PreferredName", PreferredName);

            dataObject.FieldCollection.Add("dobandagetypeid", 5);
            dataObject.FieldCollection.Add("DateOfBirth", DateOfBirth);
            dataObject.FieldCollection.Add("EthnicityId", Ethnicity);
            dataObject.FieldCollection.Add("OwnerID", OwnerID);

            dataObject.FieldCollection.Add("addressstartdate", addressstartdate);
            dataObject.FieldCollection.Add("AddressTypeId", AddressTypeId);
            dataObject.FieldCollection.Add("GenderId", GenderId);

            dataObject.FieldCollection.Add("NHSNumber", NHSNumber);

            dataObject.FieldCollection.Add("LegacyId", LegacyId);
            dataObject.FieldCollection.Add("NationalInsuranceNumber", NationalInsuranceNumber);
            dataObject.FieldCollection.Add("UniquePupilNumber", UniquePupilNumber);

            dataObject.FieldCollection.Add("PropertyName", PropertyName);
            dataObject.FieldCollection.Add("AddressLine1", PropertyNo);
            dataObject.FieldCollection.Add("AddressLine2", Street);
            dataObject.FieldCollection.Add("AddressLine3", VlgDistrict);
            dataObject.FieldCollection.Add("AddressLine4", TownCity);
            dataObject.FieldCollection.Add("AddressLine5", County);
            dataObject.FieldCollection.Add("Postcode", Postcode);

            dataObject.FieldCollection.Add("persontypeid", 3); //Person We Support
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

            return CreateRecord(dataObject);
        }

        public Guid CreatePersonRecord(Guid PersonId, string Title, string FirstName, string MiddleName, string LastName, string PreferredName, DateTime DateOfBirth,
            Guid Ethnicity, Guid OwnerID, int AddressTypeId, int GenderId, string NHSNumber, string LegacyId, string NationalInsuranceNumber, string UniquePupilNumber,
            string PropertyName = "", string PropertyNo = "", string Street = "", string VlgDistrict = "", string TownCity = "", string County = "", string Postcode = "")
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            dataObject.FieldCollection.Add("PersonId", PersonId);

            dataObject.FieldCollection.Add("Title", Title);
            dataObject.FieldCollection.Add("FirstName", FirstName);
            dataObject.FieldCollection.Add("MiddleName", MiddleName);
            dataObject.FieldCollection.Add("LastName", LastName);
            dataObject.FieldCollection.Add("PreferredName", PreferredName);

            dataObject.FieldCollection.Add("DateOfBirth", DateOfBirth);
            dataObject.FieldCollection.Add("EthnicityId", Ethnicity);
            dataObject.FieldCollection.Add("OwnerID", OwnerID);

            dataObject.FieldCollection.Add("AddressTypeId", AddressTypeId);
            dataObject.FieldCollection.Add("GenderId", GenderId);

            dataObject.FieldCollection.Add("NHSNumber", NHSNumber);

            dataObject.FieldCollection.Add("LegacyId", LegacyId);
            dataObject.FieldCollection.Add("NationalInsuranceNumber", NationalInsuranceNumber);
            dataObject.FieldCollection.Add("UniquePupilNumber", UniquePupilNumber);

            dataObject.FieldCollection.Add("PropertyName", PropertyName);
            dataObject.FieldCollection.Add("AddressLine1", PropertyNo);
            dataObject.FieldCollection.Add("AddressLine2", Street);
            dataObject.FieldCollection.Add("AddressLine3", VlgDistrict);
            dataObject.FieldCollection.Add("AddressLine4", TownCity);
            dataObject.FieldCollection.Add("AddressLine5", County);
            dataObject.FieldCollection.Add("Postcode", Postcode);

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


            return CreateRecord(dataObject);
        }

        public Guid CreatePersonRecord(
            string Title, string FirstName, string MiddleName, string LastName, string PreferredName,
            DateTime DateOfBirth, Guid Ethnicity, Guid MaritalStatus, Guid LanguageId, Guid AddressPropertyTypeId, Guid OwnerID,
            string PropertyName, string AddressLine1, string Country, string AddressLine2, string AddressLine3, string UPRN, string AddressLine4, string AddressLine5, string Postcode,
            int AddressTypeId, int AccommodationStatusId, int LivesAloneTypeId, int GenderId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

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


            return CreateRecord(dataObject);
        }

        public Guid CreatePersonRecord(
            string Title, string FirstName, string MiddleName, string LastName, string PreferredName,
            DateTime DateOfBirth, Guid Ethnicity, Guid MaritalStatus, Guid LanguageId, Guid AddressPropertyTypeId, Guid OwnerID,
            string PropertyName, string AddressLine1, string Country, string AddressLine2, string AddressLine3, string UPRN, string AddressLine4, string AddressLine5, string Postcode,
            string NHSNumber, string BusinessPhone, string Telephone2, string Telephone3, string Telephone1, string HomePhone, string MobilePhone, string PrimaryEmail, string SecondaryEmail,
            int AddressTypeId, int AccommodationStatusId, int LivesAloneTypeId, int GenderId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

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

            return CreateRecord(dataObject);
        }

        public Guid CreatePersonRecord(string Title, string FirstName, string MiddleName, string LastName, string PreferredName,
            DateTime DateOfBirth, Guid Ethnicity, Guid OwnerID, int AddressTypeId, int GenderId, bool Deceased, DateTime DateOfDeath)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            dataObject.FieldCollection.Add("Title", Title);
            dataObject.FieldCollection.Add("FirstName", FirstName);
            dataObject.FieldCollection.Add("MiddleName", MiddleName);
            dataObject.FieldCollection.Add("LastName", LastName);
            dataObject.FieldCollection.Add("PreferredName", PreferredName);

            dataObject.FieldCollection.Add("DateOfBirth", DateOfBirth);
            dataObject.FieldCollection.Add("EthnicityId", Ethnicity);
            dataObject.FieldCollection.Add("OwnerID", OwnerID);

            dataObject.FieldCollection.Add("AddressTypeId", AddressTypeId);
            dataObject.FieldCollection.Add("GenderId", GenderId);

            dataObject.FieldCollection.Add("AllergiesNotRecorded", true);
            dataObject.FieldCollection.Add("Deceased", Deceased);
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
            dataObject.FieldCollection.Add("DateOfDeath", DateOfDeath);


            return CreateRecord(dataObject);
        }

        public Guid CreatePersonRecord(string Title, string FirstName, string MiddleName, string LastName, string PreferredName,
           DateTime DateOfBirth, Guid Ethnicity, Guid OwnerID, int AddressTypeId, int GenderId, Guid PersonTargetGroupId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            dataObject.FieldCollection.Add("Title", Title);
            dataObject.FieldCollection.Add("FirstName", FirstName);
            dataObject.FieldCollection.Add("MiddleName", MiddleName);
            dataObject.FieldCollection.Add("LastName", LastName);
            dataObject.FieldCollection.Add("PreferredName", PreferredName);

            dataObject.FieldCollection.Add("DateOfBirth", DateOfBirth);
            dataObject.FieldCollection.Add("EthnicityId", Ethnicity);
            dataObject.FieldCollection.Add("OwnerID", OwnerID);

            dataObject.FieldCollection.Add("AddressTypeId", AddressTypeId);
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
            dataObject.FieldCollection.Add("PersonTargetGroupId", PersonTargetGroupId);


            return CreateRecord(dataObject);
        }

        public Guid CreatePersonRecord(string Title, string FirstName, string MiddleName, string LastName, string PreferredName,
          DateTime DateOfBirth, Guid Ethnicity, Guid OwnerID, int AddressTypeId, int GenderId, int persontypeid)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            dataObject.FieldCollection.Add("Title", Title);
            dataObject.FieldCollection.Add("FirstName", FirstName);
            dataObject.FieldCollection.Add("MiddleName", MiddleName);
            dataObject.FieldCollection.Add("LastName", LastName);
            dataObject.FieldCollection.Add("PreferredName", PreferredName);

            dataObject.FieldCollection.Add("DateOfBirth", DateOfBirth);
            dataObject.FieldCollection.Add("EthnicityId", Ethnicity);
            dataObject.FieldCollection.Add("OwnerID", OwnerID);

            dataObject.FieldCollection.Add("AddressTypeId", AddressTypeId);
            dataObject.FieldCollection.Add("GenderId", GenderId);
            dataObject.FieldCollection.Add("persontypeid", persontypeid);

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

            return CreateRecord(dataObject);
        }

        public void UpdateAnonymousForBilling(Guid PersonID, bool anonymousforbilling)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "anonymousforbilling", anonymousforbilling);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePersonRecord(Guid PersonID, bool allowemail, DateTime dateofbirth, Guid maritalstatusid, int preferredcontacttimeid, string secondaryemail, string telephone1, string title)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "allowemail", allowemail);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "dateofbirth", dateofbirth);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "maritalstatusid", maritalstatusid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "preferredcontacttimeid", preferredcontacttimeid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "secondaryemail", secondaryemail);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "telephone1", telephone1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "title", title);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePersonalDetails(Guid PersonID, string firstname, string lastname, DateTime dateofbirth, int genderid, Guid ethnicityid, string nhsnumber, string nationalinsurancenumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "firstname", firstname);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "lastname", lastname);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "dateofbirth", dateofbirth);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "genderid", genderid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ethnicityid", ethnicityid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "nhsnumber", nhsnumber);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "nationalinsurancenumber", nationalinsurancenumber);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateContactDetails(Guid PersonID, string propertyname, string addressline1, string addressline2, string addressline3, string addressline4, string addressline5, string postcode, string homephone, string mobilephone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "propertyname", propertyname);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "addressline1", addressline1);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "addressline2", addressline2);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "addressline3", addressline3);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "addressline4", addressline4);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "addressline5", addressline5);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "postcode", postcode);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "homephone", homephone);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "mobilephone", mobilephone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePersonHomePhone(Guid PersonID, string homephone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "homephone", homephone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePersonMobilePhone(Guid PersonID, string mobilephone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "mobilephone", mobilephone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePersonBusinessPhone(Guid PersonID, string businessphone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "businessphone", businessphone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAdultSafeguarding(Guid PersonID, bool AdultSafeguardingFlag)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "adultsafeguardingflag", AdultSafeguardingFlag);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAge(Guid PersonID, int Age)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "Age", Age);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePrimaryEmail(Guid PersonID, string primaryemail)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "primaryemail", primaryemail);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePreferredDay(Guid PersonID, int preferredcontactdayid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "preferredcontactdayid", preferredcontactdayid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAgeGroupId(Guid PersonID, int AgeGroupId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "AgeGroupId", AgeGroupId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAllergiesNotRecorded(Guid PersonID, bool AllergiesNotRecorded)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "AllergiesNotRecorded", AllergiesNotRecorded);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAllowEmail(Guid PersonID, bool AllowEmail)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "AllowEmail", AllowEmail);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAllowPhone(Guid PersonID, bool AllowPhone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "AllowPhone", AllowPhone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAllowSMS(Guid PersonID, bool AllowSMS)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "AllowSMS", AllowSMS);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateRetainInformationConcern(Guid PersonID, bool RetainInformationConcern)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "RetainInformationConcern", RetainInformationConcern);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDeceased(Guid PersonID, bool Deceased)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "Deceased", Deceased);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDeceased(Guid PersonID, bool Deceased, DateTime dateofdeath)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "Deceased", Deceased);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "dateofdeath", dateofdeath);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDateOfBirth(Guid PersonID, DateTime DateOfBirth)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "DateOfBirth", DateOfBirth);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDOBAndAgeTypeId(Guid PersonID, int DOBAndAgeTypeId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "DOBAndAgeTypeId", DOBAndAgeTypeId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDOBAndAgeTypeId(Guid PersonID, int DOBAndAgeTypeId, DateTime? DateOfBirth, int? Age)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "DOBAndAgeTypeId", DOBAndAgeTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DateOfBirth", DateOfBirth);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Age", Age);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDOBAndAgeTypeId(Guid PersonID, int DOBAndAgeTypeId, DateTime? DateOfBirth, int? Age, DateTime? expecteddateofbirth)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "DOBAndAgeTypeId", DOBAndAgeTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DateOfBirth", DateOfBirth);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Age", Age);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "expecteddateofbirth", expecteddateofbirth);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateEthnicityId(Guid PersonID, Guid EthnicityId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "EthnicityId", EthnicityId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateTelephone3(Guid PersonID, string telephone3)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "telephone3", telephone3);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInterpreterRequired(Guid PersonID, bool InterpreterRequired)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "InterpreterRequired", InterpreterRequired);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateIsExternalPerson(Guid PersonID, bool IsExternalPerson)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsExternalPerson", IsExternalPerson);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateIsLookedAfterChild(Guid PersonID, bool IsLookedAfterChild)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "IsLookedAfterChild", IsLookedAfterChild);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateKnownAllergies(Guid PersonID, bool KnownAllergies)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "KnownAllergies", KnownAllergies);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateLastName(Guid PersonID, string LastName)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "LastName", LastName);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateNHSNumber(Guid PersonID, string NHSNumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "NHSNumber", NHSNumber);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateLASocialCareRef(Guid PersonID, string socialservicesref)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "socialservicesref", socialservicesref);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateNationalInsuranceNumber(Guid PersonID, string nationalinsurancenumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "nationalinsurancenumber", nationalinsurancenumber);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateNHSNumberStatusId(Guid PersonID, int NHSNumberStatusId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "NHSNumberStatusId", NHSNumberStatusId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateNoKnownAllergies(Guid PersonID, bool NoKnownAllergies)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "NoKnownAllergies", NoKnownAllergies);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateOwningBusinessUnitId(Guid PersonID, Guid OwningBusinessUnitId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateResponsibleTeam(Guid PersonID, Guid OwnerId, Guid OwningBusinessUnitId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePDSIsDeferred(Guid PersonID, bool PDSIsDeferred)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PDSIsDeferred", PDSIsDeferred);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePDSNHSNoSuperseded(Guid PersonID, bool PDSNHSNoSuperseded)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "PDSNHSNoSuperseded", PDSNHSNoSuperseded);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateRecordedInError(Guid PersonID, bool RecordedInError)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "RecordedInError", RecordedInError);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateRelatedAdultSafeguardingFlag(Guid PersonID, bool RelatedAdultSafeguardingFlag)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "RelatedAdultSafeguardingFlag", RelatedAdultSafeguardingFlag);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateRelatedChildProtectionFlag(Guid PersonID, bool RelatedChildProtectionFlag)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "RelatedChildProtectionFlag", RelatedChildProtectionFlag);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateRepresentAlertOrHazard(Guid PersonID, bool RepresentAlertOrHazard)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "RepresentAlertOrHazard", RepresentAlertOrHazard);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateGenderId(Guid PersonID, int GenderId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "GenderId", GenderId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateChildProtectionFlag(Guid PersonID, bool ChildProtectionFlag)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ChildProtectionFlag", ChildProtectionFlag);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateSuppressStatementInvoices(Guid PersonID, bool SuppressStatementInvoices)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "SuppressStatementInvoices", SuppressStatementInvoices);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateUpdatePersonAddress(Guid PersonID, bool UpdatePersonAddress)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "UpdatePersonAddress", UpdatePersonAddress);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateLinkedAddress(Guid PersonID, Guid? LinkedAddressId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "LinkedAddressId", LinkedAddressId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInactiveStatus(Guid PersonID, bool? InactiveStatus)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", InactiveStatus);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateFinanceDetails(Guid PersonID,
            bool paysbydirectdebit, string bankaccountnumber, string auddisref, string bankaccountsortcode, string bankaccountname, Guid? transactiontypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "paysbydirectdebit", paysbydirectdebit);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "bankaccountnumber", bankaccountnumber);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "auddisref", auddisref);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "bankaccountsortcode", bankaccountsortcode);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "bankaccountname", bankaccountname);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "transactiontypeid", transactiontypeid);

            this.UpdateRecord(buisinessDataObject);
        }

        //Update cpsuspenddebtorinvoices field
        public void UpdateSuspendDebtorInvoicesField(Guid PersonID, bool cpsuspenddebtorinvoices)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "cpsuspenddebtorinvoices", cpsuspenddebtorinvoices);

            this.UpdateRecord(buisinessDataObject);
        }

        public void RemovePersonRestrictionFromDB(Guid PersonID)
        {
            using (CareDirectorQA_CDEntities entity = new CareDirectorQA_CDEntities())
            {
                var _record = entity.People.Where(c => c.PersonId == PersonID).FirstOrDefault();
                _record.DataRestrictionId = null;
                entity.SaveChanges();
            }
        }

        public List<Guid> GetByAll()
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.AddReturnField(query, tableName, primaryKeyName);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public void UpdateAccommodationStatus(Guid PersonID, int? accommodationstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "accommodationstatusid", accommodationstatusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePreferredInvoiceDeliveryMethod(Guid PersonID, int personpreferreddocumentsdeliverymethodid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "personpreferreddocumentsdeliverymethodid", personpreferreddocumentsdeliverymethodid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePronoun(Guid PersonID, Guid pronounsid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "pronounsid", pronounsid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePreferredTime(Guid PersonID, int preferredcontacttimeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "preferredcontacttimeid", preferredcontacttimeid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateDebtorNumber(Guid PersonId, string debtornumber)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "debtornumber1", debtornumber);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
