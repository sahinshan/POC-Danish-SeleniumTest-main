using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonClinicalRiskStatus : BaseClass
    {
        public string TableName = "PersonClinicalRiskStatus";
        public string PrimaryKeyName = "PersonClinicalRiskStatusId";


        public PersonClinicalRiskStatus()
        {
            AuthenticateUser();
        }

        public PersonClinicalRiskStatus(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonClinicalRiskStatus(Guid PersonId, Guid OwnerId, Guid ClinicalRiskStatusId, DateTime StartDate, DateTime? EndDate = null, String Notes = "clinical risk status")
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", PersonId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", OwnerId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "clinicalriskstatusid", ClinicalRiskStatusId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", StartDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", EndDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "notes", Notes);

            return this.CreateRecord(buisinessDataObject);

        }

        public List<Guid> GetPersonClinicalRiskStatusByPersonID(Guid PersonID)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonID);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetPersonClinicalRiskStatusByID(Guid PersonClinicalRiskStatusId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, PersonClinicalRiskStatusId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonClinicalRiskStatus(Guid PersonClinicalRiskStatusId)
        {
            this.DeleteRecord(TableName, PersonClinicalRiskStatusId);
        }
    }
}
