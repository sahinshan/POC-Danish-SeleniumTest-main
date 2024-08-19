using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class WebsiteUser : BaseClass
    {

        private string tableName = "WebsiteUser";
        private string primaryKeyName = "WebsiteUserId";

        public WebsiteUser()
        {
            AuthenticateUser();
        }

        public WebsiteUser(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWebsiteUser(Guid WebsiteId, string Name, string Username, string Password, bool EmailVerified, int StatusId, Guid ProfileId, string profileidtablename, string profileidname, Guid SecurityProfileId)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "WebsiteId", WebsiteId);
            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "Username", Username);
            AddFieldToBusinessDataObject(dataObject, "Password", Password);
            AddFieldToBusinessDataObject(dataObject, "EmailVerified", EmailVerified);
            AddFieldToBusinessDataObject(dataObject, "StatusId", StatusId);
            AddFieldToBusinessDataObject(dataObject, "ProfileId", ProfileId);
            AddFieldToBusinessDataObject(dataObject, "profileidtablename", profileidtablename);
            AddFieldToBusinessDataObject(dataObject, "profileidname", profileidname);
            AddFieldToBusinessDataObject(dataObject, "SecurityProfileId", SecurityProfileId);
            AddFieldToBusinessDataObject(dataObject, "IsAccountLocked", 0);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public void UpdateWebsiteUser(Guid WebsiteUserId, string Name, string Username, string Password, bool EmailVerified,
            string mobilephonenumber, int StatusId, Guid ProfileId, string profileidtablename, string profileidname, Guid? SecurityProfileId, int? twofactorauthenticationtypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "Username", Username);
            AddFieldToBusinessDataObject(buisinessDataObject, "Password", Password);
            AddFieldToBusinessDataObject(buisinessDataObject, "EmailVerified", EmailVerified);
            AddFieldToBusinessDataObject(buisinessDataObject, "mobilephonenumber", mobilephonenumber);
            AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", StatusId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ProfileId", ProfileId);
            AddFieldToBusinessDataObject(buisinessDataObject, "profileidtablename", profileidtablename);
            AddFieldToBusinessDataObject(buisinessDataObject, "profileidname", profileidname);
            AddFieldToBusinessDataObject(buisinessDataObject, "SecurityProfileId", SecurityProfileId);
            AddFieldToBusinessDataObject(buisinessDataObject, "twofactorauthenticationtypeid", twofactorauthenticationtypeid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateWebsiteUser(Guid WebsiteUserId, int FailedPasswordAttemptCount, DateTime? LastFailedPasswordAttemptDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "FailedPasswordAttemptCount", FailedPasswordAttemptCount);
            AddFieldToBusinessDataObject(buisinessDataObject, "LastFailedPasswordAttemptDate", LastFailedPasswordAttemptDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateWebsiteUser(Guid WebsiteUserId, int FailedPasswordAttemptCount, DateTime? LastFailedPasswordAttemptDate, bool isaccountlocked)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "FailedPasswordAttemptCount", FailedPasswordAttemptCount);
            AddFieldToBusinessDataObject(buisinessDataObject, "LastFailedPasswordAttemptDate", LastFailedPasswordAttemptDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "isaccountlocked", isaccountlocked);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateWebsiteUser(Guid WebsiteUserId, int FailedPasswordAttemptCount, DateTime? LastFailedPasswordAttemptDate, DateTime? lockedoutdate, bool isaccountlocked)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "FailedPasswordAttemptCount", FailedPasswordAttemptCount);
            AddFieldToBusinessDataObject(buisinessDataObject, "LastFailedPasswordAttemptDate", LastFailedPasswordAttemptDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "lockedoutdate", lockedoutdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "isaccountlocked", isaccountlocked);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateWebsiteUser(Guid WebsiteUserId, int failedpinattemptcount)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "failedpinattemptcount", failedpinattemptcount);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePassword(Guid WebsiteUserId, string Password)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "Password", Password);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateTwoFactorAuthenticationType(Guid WebsiteUserId, int? TwoFactorAuthenticationType)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "twofactorauthenticationtypeid", TwoFactorAuthenticationType);

            this.UpdateRecord(buisinessDataObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WebsiteUserId"></param>
        /// <param name="StatusId">Waiting for Approval : 1 \r\n Approved : 2 \r\n Suspended : 3 \r\n Account De-Activated : 4 </param>
        public void UpdateStatus(Guid WebsiteUserId, int StatusId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "StatusId", StatusId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAttachmentLimitDates(Guid WebsiteUserId, string AttachmentLimitDates)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "AttachmentLimitDates", AttachmentLimitDates);

            this.UpdateRecord(buisinessDataObject);
        }

        public void ResetEmailVerifiedField(Guid WebsiteUserId, bool emailverified)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "emailverified", emailverified);

            UpdateRecord(buisinessDataObject);
        }

        public void ResetLastPasswordChangedDate(Guid WebsiteUserId, DateTime lastpasswordchangeddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteUserId);

            AddFieldToBusinessDataObject(buisinessDataObject, "lastpasswordchangeddate", lastpasswordchangeddate);

            UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetByWebSiteID(Guid WebsiteId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByWebSiteIDAndUserName(Guid WebsiteId, string Username)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "WebsiteId", ConditionOperatorType.Equal, WebsiteId);
            this.BaseClassAddTableCondition(query, "Username", ConditionOperatorType.Equal, Username);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetByID(Guid WebsiteUserId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteUserId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsiteUser(Guid WebsiteUserID)
        {
            this.DeleteRecord(tableName, WebsiteUserID);
        }



    }
}
