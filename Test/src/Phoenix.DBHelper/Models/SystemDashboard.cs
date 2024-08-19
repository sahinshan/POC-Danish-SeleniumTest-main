using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class SystemDashboard : BaseClass
    {
        public string TableName { get { return "SystemDashboard"; } }
        public string PrimaryKeyName { get { return "SystemDashboardid"; } }


        public SystemDashboard()
        {
            AuthenticateUser();
        }

        public SystemDashboard(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetSystemDashboardByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetSystemDashboardByID(Guid SystemDashboardId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("SystemDashboard", false, "SystemDashboardId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "SystemDashboardId", ConditionOperatorType.Equal, SystemDashboardId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateSystemDashboard(Guid SystemDashboardId, bool AutoRefresh, int? RefreshTime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemDashboardId);
            AddFieldToBusinessDataObject(buisinessDataObject, "AutoRefresh", AutoRefresh);
            AddFieldToBusinessDataObject(buisinessDataObject, "RefreshTime", RefreshTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "visible", true);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateSystemDashboard(Guid SystemDashboardId, string LayoutXml)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemDashboardId);
            AddFieldToBusinessDataObject(buisinessDataObject, "LayoutXml", LayoutXml);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
