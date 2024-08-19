using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RequirementLastChasedOutcome : BaseClass
    {

        public string TableName = "RequirementLastChasedOutcome";
        public string PrimaryKeyName = "RequirementLastChasedOutcomeId";


        public RequirementLastChasedOutcome()
        {
            AuthenticateUser();
        }

        public RequirementLastChasedOutcome(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateRequirementLastChasedOutcome(string Name, Guid ownerid, DateTime StartDate, DateTime? EndDate = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "name", Name);
            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "startdate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "enddate", EndDate);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "validforexport", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetRequirementLastChasedOutcomeByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByRequirementLastChasedOutcomeId(Guid RequirementLastChasedOutcomeId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RequirementLastChasedOutcomeId", ConditionOperatorType.Equal, RequirementLastChasedOutcomeId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid RequirementLastChasedOutcomeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, RequirementLastChasedOutcomeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteRequirementLastChasedOutcome(Guid RequirementLastChasedOutcomeId)
        {
            this.DeleteRecord(TableName, RequirementLastChasedOutcomeId);
        }
    }
}
