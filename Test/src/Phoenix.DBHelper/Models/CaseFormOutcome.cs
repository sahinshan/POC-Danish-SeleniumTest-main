using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CaseFormOutcome : BaseClass
    {

        public string TableName = "CaseFormOutcome";
        public string PrimaryKeyName = "CaseFormOutcomeId";


        public CaseFormOutcome()
        {
            AuthenticateUser();
        }

        public CaseFormOutcome(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetCaseFormOutcomesByCaseFormID(Guid CaseFormId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseFormId", ConditionOperatorType.Equal, CaseFormId);

            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCaseFormOutcomeByID(Guid CaseFormOutcomeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseFormOutcomeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateActionOutcomeRecord(Guid ownerid, Guid personid, Guid CaseFormOutcomeTypeId, DateTime OutcomeDate, Guid CaseId, Guid CaseFormId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "CaseFormOutcomeTypeId", CaseFormOutcomeTypeId);
            AddFieldToBusinessDataObject(dataObject, "OutcomeDate", OutcomeDate);
            AddFieldToBusinessDataObject(dataObject, "CaseFormId", CaseFormId);
            AddFieldToBusinessDataObject(dataObject, "CaseId", CaseId);


            return this.CreateRecord(dataObject);
        }

        public void DeleteCaseFormOutcome(Guid CaseFormOutcomeId)
        {
            this.DeleteRecord(TableName, CaseFormOutcomeId);
        }
    }
}
