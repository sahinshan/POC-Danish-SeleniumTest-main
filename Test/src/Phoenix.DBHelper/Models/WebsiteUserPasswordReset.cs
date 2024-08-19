using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteUserPasswordReset : BaseClass
    {

        private string tableName = "WebsiteUserPasswordReset";
        private string primaryKeyName = "WebsiteUserPasswordResetId";

        public WebsiteUserPasswordReset()
        {
            AuthenticateUser();
        }

        public WebsiteUserPasswordReset(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWebsiteUserPasswordReset(Guid WebsiteUserId, DateTime? ExpireOn, DateTime? senton, string error, string resetpasswordlink)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "WebsiteUserId", WebsiteUserId);
            AddFieldToBusinessDataObject(dataObject, "ExpireOn", ExpireOn);
            AddFieldToBusinessDataObject(dataObject, "senton", senton);
            AddFieldToBusinessDataObject(dataObject, "error", error);
            AddFieldToBusinessDataObject(dataObject, "resetpasswordlink", resetpasswordlink);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public void UpdateWebsiteUserPasswordReset(Guid WebsiteUserPasswordResetId, string resetpasswordlink)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserPasswordResetId);

            AddFieldToBusinessDataObject(buisinessDataObject, "resetpasswordlink", resetpasswordlink);

            this.UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetByWebSiteUserID(Guid WebsiteUserId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteUserId", ConditionOperatorType.Equal, WebsiteUserId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid WebsiteUserPasswordResetId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteUserPasswordResetId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteUserPasswordReset(Guid WebsiteUserPasswordResetID)
        {
            this.DeleteRecord(tableName, WebsiteUserPasswordResetID);
        }



    }
}
