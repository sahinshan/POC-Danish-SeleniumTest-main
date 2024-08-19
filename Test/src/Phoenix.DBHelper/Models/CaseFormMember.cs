using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CaseFormMember : BaseClass
    {
        public string TableName { get { return "CaseFormMember"; } }
        public string PrimaryKeyName { get { return "CaseFormMemberid"; } }


        public CaseFormMember()
        {
            AuthenticateUser();
        }

        public CaseFormMember(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByCaseID(Guid CaseId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CaseId", ConditionOperatorType.Equal, CaseId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByCasePersonId(Guid PersonId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }
        public Dictionary<string, object> GetByID(Guid CaseFormMemberId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CaseFormMemberId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCaseFormMember(Guid CaseFormMemberid)
        {
            this.DeleteRecord(TableName, CaseFormMemberid);
        }
    }
}
