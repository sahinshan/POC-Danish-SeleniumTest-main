using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class Website : BaseClass
    {

        private string tableName = "Website";
        private string primaryKeyName = "WebsiteId";

        public Website()
        {
            AuthenticateUser();
        }

        public Website(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateWebsite(string Name, Guid ApplicationId, Guid UserRecordTypeId, string HomePage, string MemberHomePage, string Header, string Footer)
        {
            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "Name", Name);
            AddFieldToBusinessDataObject(dataObject, "ApplicationId", ApplicationId);
            AddFieldToBusinessDataObject(dataObject, "UserRecordTypeId", UserRecordTypeId);
            AddFieldToBusinessDataObject(dataObject, "HomePage", HomePage);
            AddFieldToBusinessDataObject(dataObject, "MemberHomePage", MemberHomePage);
            AddFieldToBusinessDataObject(dataObject, "Header", Header);
            AddFieldToBusinessDataObject(dataObject, "Footer", Footer);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", 0);

            return this.CreateRecord(dataObject);
        }

        public void UpdateWebsite(Guid WebsiteID, Guid ApplicationId, Guid UserRecordTypeId, string Name, string Logo, string HomePage, string MemberHomePage, string Header, string Footer)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ApplicationId", ApplicationId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "UserRecordTypeId", UserRecordTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Logo", Logo);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "HomePage", HomePage);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "MemberHomePage", MemberHomePage);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Header", Header);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Footer", Footer);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateTwoFactorAuthenticationInfo(Guid WebsiteID, bool enabletwofactorauthentication, int? defaultpinreceivingmethodid, int? pinexpirein, int? numberofpindigits, int? maxinvalidpinattemptallowed)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "enabletwofactorauthentication", enabletwofactorauthentication);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "defaultpinreceivingmethodid", defaultpinreceivingmethodid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "pinexpirein", pinexpirein);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "numberofpindigits", numberofpindigits);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "maxinvalidpinattemptallowed", maxinvalidpinattemptallowed);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateRobotSecurityType(Guid WebsiteID, int? robotsecuritytypeid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "robotsecuritytypeid", robotsecuritytypeid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePasswordComplexity(Guid WebsiteID, int minimumpasswordlength, int minimumnumericcharacters, int minimumspecialcharacters, int minimumuppercaseletters, string specialcharactersallowed, int minimumlowercaseletters)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "minimumpasswordlength", minimumpasswordlength);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "minimumnumericcharacters", minimumnumericcharacters);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "minimumspecialcharacters", minimumspecialcharacters);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "minimumuppercaseletters", minimumuppercaseletters);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "specialcharactersallowed", specialcharactersallowed);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "vminimumlowercaseletters", minimumlowercaseletters);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdatePasswordPolicy(Guid WebsiteID, int maximumpasswordage, int minimumpasswordage, int enforcepasswordhistory, int passwordresetexpirein)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "maximumpasswordage", maximumpasswordage);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "minimumpasswordage", minimumpasswordage);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enforcepasswordhistory", enforcepasswordhistory);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "passwordresetexpirein", passwordresetexpirein);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAdministrationInformation(Guid WebsiteID, bool emailverificationrequired, bool userapprovalrequired)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, WebsiteID);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "emailverificationrequired", emailverificationrequired);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "userapprovalrequired", userapprovalrequired);

            this.UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetWebSiteByName(string Name)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Contains, Name);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }


        public Dictionary<string, object> GetWebsiteByID(Guid WebsiteId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, WebsiteId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteWebsite(Guid WebsiteID)
        {
            this.DeleteRecord(tableName, WebsiteID);
        }



    }
}
