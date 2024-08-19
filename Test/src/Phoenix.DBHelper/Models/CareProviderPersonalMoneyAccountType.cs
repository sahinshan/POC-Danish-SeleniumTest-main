using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPersonalMoneyAccountType : BaseClass
    {

        public string TableName = "careproviderpersonalmoneyaccounttype";
        public string PrimaryKeyName = "careproviderpersonalmoneyaccounttypeid";

        public CareProviderPersonalMoneyAccountType()
        {
            AuthenticateUser();
        }

        public CareProviderPersonalMoneyAccountType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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
