using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class BusinessObjectDashboard : BaseClass
    {
        public string TableName { get { return "BusinessObjectDashboard"; } }
        public string PrimaryKeyName { get { return "BusinessObjectDashboardid"; } }


        public BusinessObjectDashboard()
        {
            AuthenticateUser();
        }

        public BusinessObjectDashboard(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetBusinessObjectDashboardByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetBusinessObjectDashboardByID(Guid BusinessObjectDashboardId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("BusinessObjectDashboard", false, "BusinessObjectDashboardId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, "BusinessObjectDashboardId", ConditionOperatorType.Equal, BusinessObjectDashboardId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateBusinessObjectDashboard(Guid BusinessObjectDashboardId, bool AutoRefresh, int? RefreshTime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BusinessObjectDashboardId);
            AddFieldToBusinessDataObject(buisinessDataObject, "AutoRefresh", AutoRefresh);
            AddFieldToBusinessDataObject(buisinessDataObject, "RefreshTime", RefreshTime);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateBusinessObjectDashboard(Guid BusinessObjectDashboardId, string LayoutXml)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, BusinessObjectDashboardId);
            AddFieldToBusinessDataObject(buisinessDataObject, "LayoutXml", LayoutXml);

            UpdateRecord(buisinessDataObject);
        }

        public void DeleteBusinessObjectDashboard(Guid BusinessObjectDashboardId)
        {
            this.DeleteRecord(TableName, BusinessObjectDashboardId);
        }
    }
}
