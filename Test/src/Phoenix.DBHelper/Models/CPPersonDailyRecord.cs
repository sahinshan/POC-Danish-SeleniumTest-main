using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPPersonDailyRecord : BaseClass
    {

        private string tableName = "CPPersonDailyRecord";
        private string primaryKeyName = "CPPersonDailyRecordId";

        public CPPersonDailyRecord()
        {
            AuthenticateUser();
        }

        public CPPersonDailyRecord(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
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


        public Dictionary<string, object> GetById(Guid CPPersonDailyRecordId, params string[] fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.AddReturnFields(query, tableName, fields);
            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPPersonDailyRecordId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



    }
}
