using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPIS : BaseClass
    {

        private string tableName = "CPIS";
        private string primaryKeyName = "CPISId";

        public CPIS()
        {
            AuthenticateUser();
        }

        public CPIS(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }



        public List<Guid> GetCPISByPersonID(Guid PersonId)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, PersonId);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetCPISByID(Guid CPISId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "CPISId", ConditionOperatorType.Equal, CPISId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }





    }
}
