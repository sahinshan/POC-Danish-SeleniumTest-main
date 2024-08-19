using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class UserDashboard : BaseClass
    {
        public string TableName { get { return "UserDashboard"; } }
        public string PrimaryKeyName { get { return "UserDashboardid"; } }


        public UserDashboard()
        {
            AuthenticateUser();
        }

        public UserDashboard(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetUserDashboardByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetUserDashboardByID(Guid UserDashboardId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("UserDashboard", false, "UserDashboardId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "UserDashboardId", ConditionOperatorType.Equal, UserDashboardId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateUserDashboard(Guid UserDashboardId, bool AutoRefresh, int? RefreshTime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, UserDashboardId);
            AddFieldToBusinessDataObject(buisinessDataObject, "AutoRefresh", AutoRefresh);
            AddFieldToBusinessDataObject(buisinessDataObject, "RefreshTime", RefreshTime);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateOwningUserId(Guid UserDashboardId, Guid OwningUserId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, UserDashboardId);
            AddFieldToBusinessDataObject(buisinessDataObject, "OwningUserId", OwningUserId);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
