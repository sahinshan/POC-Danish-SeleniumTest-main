using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ServiceProvidedRateSchedule : BaseClass
    {
        public string TableName { get { return "ServiceProvidedRateSchedule"; } }
        public string PrimaryKeyName { get { return "ServiceProvidedRateScheduleid"; } }

        public ServiceProvidedRateSchedule()
        {
            AuthenticateUser();
        }

        public ServiceProvidedRateSchedule(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateServiceProvidedRateSchedule(Guid OwnerId, Guid serviceprovidedrateperiodid, Guid serviceprovidedid, decimal rate, decimal ratebankholiday)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedrateperiodid", serviceprovidedrateperiodid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rate", rate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ratebankholiday", ratebankholiday);

            AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            AddFieldToBusinessDataObject(buisinessDataObject, "ValidForExport", false);

            AddFieldToBusinessDataObject(buisinessDataObject, "SelectAll", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "Monday", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "Tuesday", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "Wednesday", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "Thursday", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "Friday", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "Saturday", true);
            AddFieldToBusinessDataObject(buisinessDataObject, "Sunday", true);

            AddFieldToBusinessDataObject(buisinessDataObject, "timebandstart", new TimeSpan(0, 5, 0));
            AddFieldToBusinessDataObject(buisinessDataObject, "timebandend", new TimeSpan(23, 55, 0));


            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateServiceProvidedRateSchedule(Guid OwnerId, Guid serviceprovidedrateperiodid, Guid serviceprovidedid, decimal? rate, decimal? ratebankholiday,
            TimeSpan? TimeBandStart, TimeSpan? TimeBandEnd, bool? SelectAll = false, bool? Monday = false, bool? Tuesday = false, bool? Wednesday = false, bool? Thursday = false, bool? Friday = false, bool? Saturday = false, bool? Sunday = false, decimal? rateperunit = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedrateperiodid", serviceprovidedrateperiodid);
            AddFieldToBusinessDataObject(buisinessDataObject, "serviceprovidedid", serviceprovidedid);
            AddFieldToBusinessDataObject(buisinessDataObject, "rate", rate);
            AddFieldToBusinessDataObject(buisinessDataObject, "ratebankholiday", ratebankholiday);

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
            AddFieldToBusinessDataObject(buisinessDataObject, "rateperunit", rateperunit);


            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByServiceProvidedRatePeriodId(Guid ServiceProvidedRatePeriodId)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);
            this.BaseClassAddTableCondition(query, "ServiceProvidedRatePeriodId", ConditionOperatorType.Equal, ServiceProvidedRatePeriodId);
            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid ServiceProvidedRateScheduleid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ServiceProvidedRateScheduleid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }



    }
}
