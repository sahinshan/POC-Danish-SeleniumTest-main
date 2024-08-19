using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class Compliance : BaseClass
    {

        public string TableName = "compliance";
        public string PrimaryKeyName = "complianceid";

        public Compliance()
        {
            AuthenticateUser();
        }

        public Compliance(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCompliance(Guid ownerid, Guid regardingid, string regardingidtablename, string regardingidname,
            Guid complianceitemid, string complianceitemidtablename, string complianceitemidname,
            int recruitmentdocumentstatusid)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(dataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(dataObject, "complianceitemid", complianceitemid);
            AddFieldToBusinessDataObject(dataObject, "complianceitemidtablename", complianceitemidtablename);
            AddFieldToBusinessDataObject(dataObject, "complianceitemidname", complianceitemidname);
            AddFieldToBusinessDataObject(dataObject, "recruitmentdocumentstatusid", recruitmentdocumentstatusid);

            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public Guid CreateCompliance(Guid ownerid, Guid regardingid, string regardingidtablename, string regardingidname,
            Guid complianceitemid, string complianceitemidtablename, string complianceitemidname, int recruitmentdocumentstatusid, string referencenumber,
            DateTime? requesteddate, DateTime? completeddate, DateTime? validfromdate, DateTime? validtodate,
            Guid? requestedbyid, Guid? completedbyid, int? additionalattributeid = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "regardingid", regardingid);
            AddFieldToBusinessDataObject(dataObject, "regardingidtablename", regardingidtablename);
            AddFieldToBusinessDataObject(dataObject, "regardingidname", regardingidname);
            AddFieldToBusinessDataObject(dataObject, "complianceitemid", complianceitemid);
            AddFieldToBusinessDataObject(dataObject, "complianceitemidtablename", complianceitemidtablename);
            AddFieldToBusinessDataObject(dataObject, "complianceitemidname", complianceitemidname);
            AddFieldToBusinessDataObject(dataObject, "recruitmentdocumentstatusid", recruitmentdocumentstatusid);

            AddFieldToBusinessDataObject(dataObject, "referencenumber", referencenumber);
            AddFieldToBusinessDataObject(dataObject, "requesteddate", requesteddate);
            AddFieldToBusinessDataObject(dataObject, "completeddate", completeddate);
            AddFieldToBusinessDataObject(dataObject, "validfromdate", validfromdate);
            AddFieldToBusinessDataObject(dataObject, "validtodate", validtodate);
            AddFieldToBusinessDataObject(dataObject, "requestedbyid", requestedbyid);
            AddFieldToBusinessDataObject(dataObject, "completedbyid", completedbyid);

            AddFieldToBusinessDataObject(dataObject, "additionalattributeid", additionalattributeid);

            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "ValidForExport", false);

            return this.CreateRecord(dataObject);
        }

        public List<Guid> GetComplianceById(Guid complianceid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "complianceid", ConditionOperatorType.Equal, complianceid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetComplianceByName(string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByComplianceID(Guid complianceid, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, complianceid);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetComplianceByRegardingId(Guid regardingid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, regardingid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetComplianceByRegardingIdAndAdditionalAttributeId(Guid regardingid, int additionalattributeid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            AddReturnField(query, TableName, PrimaryKeyName);

            BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, regardingid);
            BaseClassAddTableCondition(query, "additionalattributeid", ConditionOperatorType.Equal, additionalattributeid);
            AddReturnField(query, TableName, PrimaryKeyName);
            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByRegardingIdAndComplianceItemId(Guid regardingid, Guid complianceitemid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            AddReturnField(query, TableName, PrimaryKeyName);

            BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, regardingid);
            BaseClassAddTableCondition(query, "complianceitemid", ConditionOperatorType.Equal, complianceitemid);
            AddReturnField(query, TableName, PrimaryKeyName);
            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetComplianceByApplicantIdAndComplianceName(Guid applicantId, string name)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "regardingid", ConditionOperatorType.Equal, applicantId);
            this.BaseClassAddTableCondition(query, "name", ConditionOperatorType.Equal, name);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetComplianceByApplicantName(string applicantName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "regardingidname", ConditionOperatorType.Equal, applicantName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetAll()
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteCompliance(Guid complianceid)
        {
            this.DeleteRecord(TableName, complianceid);
        }

        public void UpdateCompliance(Guid ComplianceID, int referencenumber, string refereeName, DateTime requestedDate, DateTime completedDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ComplianceID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ReferenceNumber", referencenumber);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RefereeName", refereeName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RequestedDate", requestedDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CompletedDate", completedDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCompliance(Guid ComplianceID, int referencenumber, string refereeName, DateTime requestedDate, Guid requestedById, DateTime completedDate, Guid completedById)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ComplianceID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ReferenceNumber", referencenumber);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RefereeName", refereeName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RequestedDate", requestedDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CompletedDate", completedDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "requestedById", requestedById);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "completedById", completedById);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateInactiveStatus(Guid ComplianceID, bool? InactiveStatus)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ComplianceID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "Inactive", InactiveStatus);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateAdditionalAttributeId(Guid ComplianceID, int AdditionalAttributeId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ComplianceID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "AdditionalAttributeId", AdditionalAttributeId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateOverrideDateField(Guid ComplianceID, DateTime overridedate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ComplianceID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "overridedate", overridedate);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateOverrideByField(Guid ComplianceID, Guid overridebyid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ComplianceID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "overridebyid", overridebyid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateOverrideReason(Guid ComplianceID, Guid overridereason)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ComplianceID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "overridebyid", overridereason);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateCompliance(Guid ComplianceID, int referencenumber, string refereeName, DateTime requestedDate, Guid requestedById, DateTime? completedDate, Guid? completedById, DateTime? ValidFromDate = null, DateTime? ValidToDate = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ComplianceID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ReferenceNumber", referencenumber);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RefereeName", refereeName);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "RequestedDate", requestedDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "requestedById", requestedById);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "CompletedDate", completedDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "completedById", completedById);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidFromDate", ValidFromDate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ValidToDate", ValidToDate);

            this.UpdateRecord(buisinessDataObject);
        }

    }
}