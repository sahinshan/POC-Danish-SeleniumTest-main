using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPPersonToileting : BaseClass
    {


        private string tableName = "cppersonToileting";
        private string primaryKeyName = "cppersonToiletingid";

        public CPPersonToileting()
        {
            AuthenticateUser();
        }

        public CPPersonToileting(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByPersonId(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetById(Guid cppersonToiletingid, params string[] fields)
        {
            System.Threading.Thread.Sleep(1000);
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.AddReturnFields(query, tableName, fields);
            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, cppersonToiletingid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



    }
}
