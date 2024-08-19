using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPPersonPainManagement : BaseClass
    {


        private string tableName = "cppersonpainmanagement";
        private string primaryKeyName = "cppersonpainmanagementid";

        public CPPersonPainManagement()
        {
            AuthenticateUser();
        }

        public CPPersonPainManagement(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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


        public Dictionary<string, object> GetById(Guid CPPersonPainManagementid, params string[] fields)
        {
            System.Threading.Thread.Sleep(1000);
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.AddReturnFields(query, tableName, fields);
            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPPersonPainManagementid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



    }
}
