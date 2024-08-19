using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonDNAR : BaseClass
    {

        public string TableName = "PersonDNAR";
        public string PrimaryKeyName = "PersonDNARId";


        public PersonDNAR()
        {
            AuthenticateUser();
        }

        public PersonDNAR(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonDNARRecord(Guid ownerId, String title,
             bool Inactive, Guid PersonId, String additionalinformation, DateTime completeddate, Guid DNARcompletedbyid, DateTime dnardatetime, int DNARAgeStatusId = 1, bool interestharmcprbenefit = false, bool cprrefused = false,
             bool naturaldeath = false, bool other = false, int discusseddecisionid = 1, int DNARreviewdecisionid = 3, bool canceldecision = false,
             int adultcprdeceisionid = 1)

        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", ownerId);
            AddFieldToBusinessDataObject(businessDataObject, "title", title);
            AddFieldToBusinessDataObject(businessDataObject, "Inactive", false);
            AddFieldToBusinessDataObject(businessDataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(businessDataObject, "additionalinformation", additionalinformation);
            AddFieldToBusinessDataObject(businessDataObject, "completeddate", completeddate);
            AddFieldToBusinessDataObject(businessDataObject, "DNARcompletedbyid", DNARcompletedbyid);

            AddFieldToBusinessDataObject(businessDataObject, "dnardatetime", dnardatetime);


            return this.CreateRecord(businessDataObject);
        }

        public Guid CreateInactivePersonDNARRecord(Guid ownerId, String title,
            bool Inactive, Guid PersonId, String additionalinformation, DateTime cancelleddate, DateTime completeddate, bool CancelDecision, Guid cancelledbyid, Guid DNARcompletedbyid, DateTime dnardatetime, int DNARAgeStatusId = 1, bool interestharmcprbenefit = false, bool cprrefused = false,
            bool naturaldeath = false, bool other = false, int discusseddecisionid = 1, int DNARreviewdecisionid = 3,
            int adultcprdeceisionid = 1)

        {
            var businessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(businessDataObject, "OwnerId", ownerId);
            AddFieldToBusinessDataObject(businessDataObject, "title", title);
            AddFieldToBusinessDataObject(businessDataObject, "Inactive", true);
            AddFieldToBusinessDataObject(businessDataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(businessDataObject, "additionalinformation", additionalinformation);
            AddFieldToBusinessDataObject(businessDataObject, "CancelDecision", true);
            AddFieldToBusinessDataObject(businessDataObject, "cancelleddate", cancelleddate);
            AddFieldToBusinessDataObject(businessDataObject, "completeddate", completeddate);
            AddFieldToBusinessDataObject(businessDataObject, "cancelledbyid", cancelledbyid);
            AddFieldToBusinessDataObject(businessDataObject, "DNARcompletedbyid", DNARcompletedbyid);

            AddFieldToBusinessDataObject(businessDataObject, "dnardatetime", dnardatetime);


            return this.CreateRecord(businessDataObject);
        }

        public void UpdateCancelDecision(Guid PersonDNARId, bool CancelDecision)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonDNARId);
            //this.AddFieldToBusinessDataObject(buisinessDataObject, "trainingitemid", trainingitemid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CancelDecision", 1);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByPersonID(Guid PersonID, bool inactive)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);
            this.BaseClassAddTableCondition(query, "inactive", ConditionOperatorType.Equal, inactive);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid PersonDNARId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonDNARId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonDNAR(Guid PersonDNARId)
        {
            this.DeleteRecord(TableName, PersonDNARId);
        }




    }
}
