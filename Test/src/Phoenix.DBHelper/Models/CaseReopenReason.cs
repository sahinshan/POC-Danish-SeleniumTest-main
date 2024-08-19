using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CaseReopenReason : BaseClass
    {

        public string TableName = "CaseReopenReason";
        public string PrimaryKeyName = "CaseReopenReasonId";


        public CaseReopenReason()
        {
            AuthenticateUser();
        }

        public CaseReopenReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCaseReopenReason(Guid ownerid, string Name, string Code, DateTime StartDate, int BusinessTypeId)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "BusinessTypeId", BusinessTypeId);

            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);


            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetCaseReopenReasonByID(Guid CaseReopenReasonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseReopenReasonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCaseReopenReason(Guid CaseReopenReasonId)
        {
            this.DeleteRecord(TableName, CaseReopenReasonId);
        }
    }
}
