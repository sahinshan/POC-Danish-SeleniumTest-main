using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteAnnouncement : BaseClass
    {

        private string tableName = "WebsiteAnnouncement";
        private string primaryKeyName = "WebsiteAnnouncementId";

        public WebsiteAnnouncement()
        {
            AuthenticateUser();
        }

        public WebsiteAnnouncement(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWebsiteAnnouncement(Guid WebsiteId, string Name, string Contents, DateTime? ExpiresOn, DateTime? Published, int Statusid)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "WebsiteId", WebsiteId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Contents", Contents);
            AddFieldToBusinessDataObject(dataObject, "ExpiresOn", ExpiresOn);
            AddFieldToBusinessDataObject(dataObject, "Published", Published);
            AddFieldToBusinessDataObject(dataObject, "Statusid", Statusid);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }


        public void UpdateWebsiteAnnouncement(Guid WebsiteAnnouncementID, string Name, string Contents, DateTime? ExpiresOn, DateTime? Published, int Statusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteAnnouncementID);

            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "Contents", Contents);
            AddFieldToBusinessDataObject(buisinessDataObject, "ExpiresOn", ExpiresOn);
            AddFieldToBusinessDataObject(buisinessDataObject, "Published", Published);
            AddFieldToBusinessDataObject(buisinessDataObject, "Statusid", Statusid);

            this.UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetByWebSiteID(Guid WebsiteId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByWebSiteIDAndName(Guid WebsiteId, string Name)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);
            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Contains, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid WebsiteAnnouncementId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteAnnouncementId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteAnnouncement(Guid WebsiteAnnouncementID)
        {
            this.DeleteRecord(tableName, WebsiteAnnouncementID);
        }



    }
}
