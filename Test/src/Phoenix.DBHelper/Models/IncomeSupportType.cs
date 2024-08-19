using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class IncomeSupportType : BaseClass
    {
        public IncomeSupportType()
        {
            AuthenticateUser();
        }

        public IncomeSupportType(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject("IncomeSupportType", false, "IncomeSupportTypeId");

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, "IncomeSupportType", "IncomeSupportTypeid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "IncomeSupportTypeId");
        }

        public List<Guid> GetIncomeSupportTypeByID(Guid IncomeSupportTypeId)
        {
            DataQuery query = this.GetDataQueryObject("IncomeSupportType", false, "IncomeSupportTypeId");
            this.BaseClassAddTableCondition(query, "IncomeSupportTypeId", ConditionOperatorType.Equal, IncomeSupportTypeId);
            this.AddReturnField(query, "IncomeSupportType", "IncomeSupportTypeid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "IncomeSupportTypeId");
        }




    }
}
