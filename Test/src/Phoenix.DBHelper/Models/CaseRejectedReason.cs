using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CaseRejectedReason : BaseClass
    {
        public string TableName = "CaseRejectedReason";
        public string PrimaryKeyName = "CaseRejectedReasonid";


        public CaseRejectedReason()
        {
            AuthenticateUser();
        }

        public CaseRejectedReason(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCaseRejectedReason(string Name, string Code, Guid OwnerId, DateTime StartDate, Guid caseclosurereasonid, int businesstypeid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Code", Code);
            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "startdate", StartDate);
            AddFieldToBusinessDataObject(dataObject, "caseclosurereasonid", caseclosurereasonid);
            AddFieldToBusinessDataObject(dataObject, "businesstypeid", businesstypeid);

            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CaseRejectedReasonId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseRejectedReasonId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}
