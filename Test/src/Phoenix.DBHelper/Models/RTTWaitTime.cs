using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class RTTWaitTime : BaseClass
    {
        public string TableName = "RTTWaitTime";
        public string PrimaryKeyName = "RTTWaitTimeId";

        public RTTWaitTime()
        {
            AuthenticateUser();
        }

        public RTTWaitTime(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByRelatedCase(Guid caseid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "caseid", ConditionOperatorType.Equal, caseid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid RTTWaitTimeId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, RTTWaitTimeId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


    }
}
