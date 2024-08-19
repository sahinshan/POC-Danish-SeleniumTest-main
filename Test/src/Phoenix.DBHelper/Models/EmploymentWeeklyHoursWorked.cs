using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class EmploymentWeeklyHoursWorked : BaseClass
    {

        private string tableName = "EmploymentWeeklyHoursWorked";
        private string primaryKeyName = "EmploymentWeeklyHoursWorkedId";

        public EmploymentWeeklyHoursWorked()
        {
            AuthenticateUser();
        }

        public EmploymentWeeklyHoursWorked(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, tableName, primaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }



    }
}
