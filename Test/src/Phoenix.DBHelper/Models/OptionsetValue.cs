using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class OptionsetValue : BaseClass
    {
        public string TableName = "OptionsetValue";
        public string PrimaryKeyName = "OptionsetValueId";


        public OptionsetValue()
        {
            AuthenticateUser();
        }

        public OptionsetValue(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetOptionSetValueIdByOptionSetId_Text(Guid OptionSetId, string Text)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "OptionSetId", ConditionOperatorType.Equal, OptionSetId);
            this.BaseClassAddTableCondition(query, "Text", ConditionOperatorType.Equal, Text);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }


        public Dictionary<string, object> GetOptionsetValueByID(Guid OptionsetValueId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, OptionsetValueId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

    }
}