using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsitePage : BaseClass
    {

        private string tableName = "WebsitePage";
        private string primaryKeyName = "WebsitePageId";

        public WebsitePage()
        {
            AuthenticateUser();
        }

        public WebsitePage(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWebsitePage(string Name, Guid WebsiteId, Guid? ParentPageID, Guid? LabelId, string LayoutJson)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "WebsiteId", WebsiteId);

            if (ParentPageID.HasValue)
                AddFieldToBusinessDataObject(dataObject, "ParentPageID", ParentPageID.Value);

            if (LabelId.HasValue)
                AddFieldToBusinessDataObject(dataObject, "LabelId", LabelId.Value);

            AddFieldToBusinessDataObject(dataObject, "LayoutJson", LayoutJson);

            AddFieldToBusinessDataObject(dataObject, "IsSecure", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public void UpdateWebsitePage(Guid WebsitePageID, string Name, Guid? ParentPageID, Guid? LabelId, string LayoutJson)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsitePageID);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            if (ParentPageID.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "ParentPageID", ParentPageID.Value);
            else
                AddFieldToBusinessDataObject(buisinessDataObject, "ParentPageID", null);
            if (LabelId.HasValue)
                AddFieldToBusinessDataObject(buisinessDataObject, "LabelId", LabelId.Value);
            else
                AddFieldToBusinessDataObject(buisinessDataObject, "LabelId", null);
            AddFieldToBusinessDataObject(buisinessDataObject, "LayoutJson", LayoutJson);
            AddFieldToBusinessDataObject(buisinessDataObject, "IsSecure", 0);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateParentPage(Guid WebsitePageID, Guid? ParentPageID)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsitePageID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ParentPageID", ParentPageID);
            this.UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetByWebSiteID(Guid WebsiteId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByWebSiteIDAndPageName(Guid WebsiteId, string Name)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);
            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Contains, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByWebSiteIDAndExactPageName(Guid WebsiteId, string Name)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);
            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }




        public Dictionary<string, object> GetByID(Guid WebsitePageId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsitePageId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsitePage(Guid WebsitePageID)
        {
            this.DeleteRecord(tableName, WebsitePageID);
        }



    }
}
