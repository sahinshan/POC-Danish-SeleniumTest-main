using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{


    public class CPExpressBookingResult : BaseClass
    {

        public string TableName = "CPExpressBookingResult";
        public string PrimaryKeyName = "CPExpressBookingResultid";

        public CPExpressBookingResult(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByExpressBookingCriteriaID(Guid cpexpressbookingcriteriaid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "cpexpressbookingcriteriaid", ConditionOperatorType.Equal, cpexpressbookingcriteriaid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetById(Guid CPExpressBookingResultid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPExpressBookingResultid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByExpressBookingCriteriaIDAndCPBookingScheduleID(Guid cpexpressbookingcriteriaid, Guid cpbookingscheduleid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "cpexpressbookingcriteriaid", ConditionOperatorType.Equal, cpexpressbookingcriteriaid);
            this.BaseClassAddTableCondition(query, "cpbookingscheduleid", ConditionOperatorType.Equal, cpbookingscheduleid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

    }
}
