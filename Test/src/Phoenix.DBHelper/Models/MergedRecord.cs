using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class MergedRecord : BaseClass
    {

        public string TableName = "MergedRecord";
        public string PrimaryKeyName = "MergedRecordId";


        public MergedRecord()
        {
            AuthenticateUser();
        }

        public MergedRecord(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }



        public List<Guid> GetMergedRecordByMasterRecordID(Guid MasterRecordId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "MasterRecordId", ConditionOperatorType.Equal, MasterRecordId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetMergedRecordByID(Guid MergedRecordId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, MergedRecordId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }




    }
}
