using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class UserChart : BaseClass
    {
        public string TableName { get { return "UserChart"; } }
        public string PrimaryKeyName { get { return "UserChartid"; } }


        public UserChart()
        {
            AuthenticateUser();
        }

        public UserChart(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetUserChartByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetUserChartByID(Guid UserChartId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("UserChart", false, "UserChartId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "UserChartId", ConditionOperatorType.Equal, UserChartId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteUserChart(Guid UserChartID)
        {
            this.DeleteRecord(TableName, UserChartID);
        }
    }
}
