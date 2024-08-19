using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPersonalMoneyAccountEntryType : BaseClass
    {

        public string TableName = "CareProviderPersonalMoneyAccountEntryType";
        public string PrimaryKeyName = "CareProviderPersonalMoneyAccountEntryTypeid";

        public CareProviderPersonalMoneyAccountEntryType()
        {
            AuthenticateUser();
        }

        public CareProviderPersonalMoneyAccountEntryType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
