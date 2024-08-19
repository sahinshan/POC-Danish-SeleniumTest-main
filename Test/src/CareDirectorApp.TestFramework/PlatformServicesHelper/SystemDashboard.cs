using CareDirector.Sdk.Enums;
using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareDirectorApp.TestFramework
{
    public class SystemDashboard : BaseClass
    {
        public string TableName { get { return "SystemDashboard"; } }
        public string PrimaryKeyName { get { return "SystemDashboardId"; } }


        public SystemDashboard(string AccessToken)
        {
            this.AccessToken = AccessToken;
        }

        public Dictionary<string, object> GetSystemDashboardByID(string SystemDashboardId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject("SystemDashboard", false, "SystemDashboardId");
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.AddTableCondition(query, "SystemDashboardId", ConditionOperatorType.Equal, SystemDashboardId);
            
            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateLayoutXML(Guid SystemDashboardId, string LayoutXml)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, SystemDashboardId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "LayoutXml", LayoutXml);

            this.UpdateRecord(buisinessDataObject);
        }
    }
}
