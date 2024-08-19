using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceProvisionStatus : BaseClass
    {
        public ServiceProvisionStatus()
        {
            AuthenticateUser();
        }

        public ServiceProvisionStatus(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetServiceProvisionStatusByName(string ServiceProvisionStatusName)
        {
            DataQuery query = this.GetDataQueryObject("ServiceProvisionStatus", false, "ServiceProvisionStatusId");
            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, ServiceProvisionStatusName);
            this.AddReturnField(query, "ServiceProvisionStatus", "ServiceProvisionStatusid");


            return this.ExecuteDataQueryAndExtractGuidFields(query, "ServiceProvisionStatusid");
        }



    }
}
