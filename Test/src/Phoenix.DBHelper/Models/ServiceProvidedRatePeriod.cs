using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceProvidedRatePeriod : BaseClass
    {
        public string TableName { get { return "ServiceProvidedRatePeriod"; } }
        public string PrimaryKeyName { get { return "ServiceProvidedRatePeriodid"; } }

        public ServiceProvidedRatePeriod()
        {
            AuthenticateUser();
        }

        public ServiceProvidedRatePeriod(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceProvidedRatePeriod(Guid OwnerId, Guid serviceprovidedid, Guid rateunitid, DateTime startdate, int approvalstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "approvalstatusid", approvalstatusid);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "CapacityCanBeExceeded", false);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceProvidedRatePeriod(Guid OwnerId, Guid serviceprovidedid, Guid rateunitid, DateTime startdate, DateTime? endDate = null, int approvalstatusid = 1)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", endDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "approvalstatusid", approvalstatusid);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "CapacityCanBeExceeded", false);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceProvidedRatePeriod(Guid OwnerId, Guid serviceprovidedid, Guid rateunitid, DateTime startdate, DateTime? endDate, string capacity, Guid glCodeId, bool CapacityCanBeExceeded = false, int approvalstatusid = 1)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rateunitid", rateunitid);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", endDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "approvalstatusid", approvalstatusid);
            AddFieldToBusinessDataObject(buisinessDataObject, "capacity", capacity);
            AddFieldToBusinessDataObject(buisinessDataObject, "glcodeid", glCodeId);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "CapacityCanBeExceeded", CapacityCanBeExceeded);

            return CreateRecord(buisinessDataObject);
        }

        public void UpdateApprovalStatus(Guid ServiceProvidedRatePeriodId, int approvalstatusid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ServiceProvidedRatePeriodId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "approvalstatusid", approvalstatusid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateEndDate(Guid ServiceProvidedRatePeriodId, DateTime? endDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ServiceProvidedRatePeriodId);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", endDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public Dictionary<string, object> GetByID(Guid ServiceProvidedRatePeriodid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceProvidedRatePeriodid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByServiceProvidedID(Guid ServiceProvidedId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "ServiceProvidedId", ConditionOperatorType.Equal, ServiceProvidedId);
            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByServiceProvidedID(Guid ServiceProvidedId, int ApprovalStatusId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "ServiceProvidedId", ConditionOperatorType.Equal, ServiceProvidedId);
            this.BaseClassAddTableCondition(query, "ApprovalStatusId", ConditionOperatorType.Equal, ApprovalStatusId);
            query.Orders.Add(new OrderBy("CreatedOn", SortOrder.Descending, TableName));
            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteServiceProvidedRatePeriod(Guid ServiceProvidedRatePeriodID)
        {
            this.DeleteRecord(TableName, ServiceProvidedRatePeriodID);
        }
    }
}
