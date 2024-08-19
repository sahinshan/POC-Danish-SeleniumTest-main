using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceProvisionRateSchedule : BaseClass
    {
        private string TableName = "ServiceProvisionRateSchedule";
        private string PrimaryKeyName = "ServiceProvisionRateScheduleId";

        public ServiceProvisionRateSchedule()
        {
            AuthenticateUser();
        }

        public ServiceProvisionRateSchedule(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceProvisionRateSchedule(Guid OwnerId, Guid PersonId, Guid ServiceProvisionId, Guid ServiceProvisionRatePeriodId, decimal? Rate, decimal? RateBankHoliday,
            bool SelectAll = false, bool Monday = false, bool Tuesday = false, bool Wednesday = false, bool Thursday = false, bool Friday = false, bool Saturday = false, bool Sunday = false, string Notes = "Automation Notes")
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceProvisionId", ServiceProvisionId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceProvisionRatePeriodId", ServiceProvisionRatePeriodId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Rate", Rate);
            AddFieldToBusinessDataObject(buisinessDataObject, "RateBankHoliday", RateBankHoliday);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            AddFieldToBusinessDataObject(buisinessDataObject, "SelectAll", SelectAll);
            AddFieldToBusinessDataObject(buisinessDataObject, "Monday", Monday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Tuesday", Tuesday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Wednesday", Wednesday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Thursday", Thursday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Friday", Friday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Saturday", Saturday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Sunday", Sunday);

            AddFieldToBusinessDataObject(buisinessDataObject, "Notes", Notes);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceProvisionRateSchedule(Guid OwnerId, Guid PersonId, Guid ServiceProvisionId, Guid ServiceProvisionRatePeriodId, decimal? Rate, decimal? RateBankHoliday,
            TimeSpan? TimeBandStart, TimeSpan? TimeBandEnd, bool SelectAll = false, bool Monday = false, bool Tuesday = false, bool Wednesday = false, bool Thursday = false, bool Friday = false, bool Saturday = false, bool Sunday = false, string Notes = "Automation Notes")
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "PersonId", PersonId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceProvisionId", ServiceProvisionId);
            AddFieldToBusinessDataObject(buisinessDataObject, "ServiceProvisionRatePeriodId", ServiceProvisionRatePeriodId);
            AddFieldToBusinessDataObject(buisinessDataObject, "Rate", Rate);
            AddFieldToBusinessDataObject(buisinessDataObject, "RateBankHoliday", RateBankHoliday);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            AddFieldToBusinessDataObject(buisinessDataObject, "SelectAll", SelectAll);
            AddFieldToBusinessDataObject(buisinessDataObject, "Monday", Monday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Tuesday", Tuesday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Wednesday", Wednesday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Thursday", Thursday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Friday", Friday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Saturday", Saturday);
            AddFieldToBusinessDataObject(buisinessDataObject, "Sunday", Sunday);

            AddFieldToBusinessDataObject(buisinessDataObject, "timebandstart", TimeBandStart);
            AddFieldToBusinessDataObject(buisinessDataObject, "timebandend", TimeBandEnd);

            AddFieldToBusinessDataObject(buisinessDataObject, "Notes", Notes);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByServiceProvisionRatePeriodId(Guid ServiceProvisionRatePeriodId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "ServiceProvisionRatePeriodId", ConditionOperatorType.Equal, ServiceProvisionRatePeriodId);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ServiceProvisionRateScheduleId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceProvisionRateScheduleId);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteServiceProvisionRateScheduleRecord(Guid ServiceProvisionRateScheduleId)
        {
            this.DeleteRecord(TableName, ServiceProvisionRateScheduleId);
        }

    }
}
