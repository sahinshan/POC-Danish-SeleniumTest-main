using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CareProviderPersonContractServiceChargePerWk : BaseClass
    {

        public string TableName = "CareProviderPersonContractServiceChargePerWk";
        public string PrimaryKeyName = "CareProviderPersonContractServiceChargePerWkId";


        public CareProviderPersonContractServiceChargePerWk()
        {
            AuthenticateUser();
        }

        public CareProviderPersonContractServiceChargePerWk(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetByCareProviderPersonContractServiceId(Guid CareProviderPersonContractServiceId)

        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CareProviderPersonContractServiceId", ConditionOperatorType.Equal, CareProviderPersonContractServiceId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
