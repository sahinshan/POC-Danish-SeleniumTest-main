using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceProvisionRatePeriod : BaseClass
    {
        private string TableName = "ServiceProvisionRatePeriod";
        private string PrimaryKeyName = "ServiceProvisionRatePeriodId";

        public ServiceProvisionRatePeriod()
        {
            AuthenticateUser();
        }

        public ServiceProvisionRatePeriod(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceProvisionRatePeriod(Guid OwnerId, Guid PersonId, string Name, Guid ServiceProvisionId, Guid RateUnitId, DateTime StartDate, DateTime? EndDate, int ApprovalStatusId = 1)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Name", Name);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceProvisionId", ServiceProvisionId);
            AddFieldToBusinessDataObject(buisinessDataObject, "RateUnitId", RateUnitId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ApprovalStatusId", ApprovalStatusId);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartDate", StartDate);
            AddFieldToBusinessDataObject(buisinessDataObject, "EndDate", EndDate);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByName(string Name)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Name", ConditionOperatorType.Equal, Name);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByServiceProvisionId(Guid ServiceProvisionId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ServiceProvisionId", ConditionOperatorType.Equal, ServiceProvisionId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public List<Guid> GetByServiceProvisionRatePeriodId(Guid ServiceProvisionRatePeriod)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceProvisionRatePeriod);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByServiceProvisionRatePeriodId(Guid ServiceProvisionRatePeriodId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceProvisionRatePeriodId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void UpdateStatus(Guid ServiceProvisionRatePeriodId, int ApprovalStatusId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, ServiceProvisionRatePeriodId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ApprovalStatusId", ApprovalStatusId);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteServiceProvisionRatePeriodRecord(Guid ServiceProvisionRatePeriodId)
        {
            this.DeleteRecord(TableName, ServiceProvisionRatePeriodId);
        }
    }
}
