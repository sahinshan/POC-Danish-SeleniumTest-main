using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SMS : BaseClass
    {

        public string TableName = "SMS";
        public string PrimaryKeyName = "smsid";


        public SMS()
        {
            AuthenticateUser();
        }

        public SMS(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetSMSByRecipientId(Guid RecipientId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RecipientId", ConditionOperatorType.Equal, RecipientId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetSMSByID(Guid SMSId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, SMSId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }


        public void DeleteSMS(Guid SMSId)
        {
            this.DeleteRecord(TableName, SMSId);
        }
    }
}
