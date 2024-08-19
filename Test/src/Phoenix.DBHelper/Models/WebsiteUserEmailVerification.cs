using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteUserEmailVerification : BaseClass
    {

        private string tableName = "WebsiteUserEmailVerification";
        private string primaryKeyName = "WebsiteUserEmailVerificationId";

        public WebsiteUserEmailVerification()
        {
            AuthenticateUser();
        }

        public Guid CreateWebsiteUserEmailVerification(Guid WebsiteUserId, DateTime SentOn, string Link)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "WebsiteUserId", WebsiteUserId);
            AddFieldToBusinessDataObject(dataObject, "SentOn", SentOn);
            AddFieldToBusinessDataObject(dataObject, "Link", Link);

            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public void UpdateWebsiteUserEmailVerification(Guid WebsiteUserEmailVerificationId, string Link)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserEmailVerificationId);

            AddFieldToBusinessDataObject(buisinessDataObject, "Link", Link);

            this.UpdateRecord(buisinessDataObject);
        }

        public WebsiteUserEmailVerification(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }


        public List<Guid> GetByWebSiteUserID(Guid WebsiteUserId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            BaseClassAddTableCondition(query, "WebsiteUserId", ConditionOperatorType.Equal, WebsiteUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid WebsiteUserEmailVerificationId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteUserEmailVerificationId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteUserEmailVerification(Guid WebsiteUserEmailVerificationID)
        {
            this.DeleteRecord(tableName, WebsiteUserEmailVerificationID);
        }



    }
}
