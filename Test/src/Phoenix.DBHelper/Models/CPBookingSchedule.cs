using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPBookingSchedule : BaseClass
    {

        public string TableName = "CPBookingSchedule";
        public string PrimaryKeyName = "CPBookingScheduleId";


        public CPBookingSchedule()
        {
            AuthenticateUser();
        }

        public CPBookingSchedule(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Dictionary<string, object> GetById(Guid CPBookingScheduleId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CPBookingScheduleId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public Guid CreateCPBookingSchedule(Guid OwnerId, Guid BookingTypeId,
            int Frequency, int StartDayOfWeekId, int EndDayOfWeekId,
            TimeSpan StartTime, TimeSpan EndTime,
            Guid ProviderId, string Comments, Guid? careproviderserviceid = null)
        {
            var dataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "OwnerId", OwnerId);
            AddFieldToBusinessDataObject(dataObject, "BookingTypeId", BookingTypeId);
            AddFieldToBusinessDataObject(dataObject, "Frequency", Frequency);
            AddFieldToBusinessDataObject(dataObject, "StartDayOfWeekId", StartDayOfWeekId);
            AddFieldToBusinessDataObject(dataObject, "EndDayOfWeekId", EndDayOfWeekId);
            AddFieldToBusinessDataObject(dataObject, "StartTime", StartTime);
            AddFieldToBusinessDataObject(dataObject, "EndTime", EndTime);
            AddFieldToBusinessDataObject(dataObject, "Locationid", ProviderId);
            AddFieldToBusinessDataObject(dataObject, "Comments", Comments);

            if (careproviderserviceid.HasValue)
                AddFieldToBusinessDataObject(dataObject, "careproviderserviceid", careproviderserviceid.Value);

            AddFieldToBusinessDataObject(dataObject, "genderpreferenceid", 1);
            AddFieldToBusinessDataObject(dataObject, "Inactive", false);
            AddFieldToBusinessDataObject(dataObject, "expressbookonpublicholidaydefaultid", 1);
            AddFieldToBusinessDataObject(dataObject, "IsDeleted", false);

            return this.CreateRecord(dataObject);
        }

        public void UpdateOccurenceInformation(Guid CPBookingScheduleId,
            int frequency, DateTime? nextoccurrencedate, DateTime? firstoccurrencedate, DateTime? lastoccurrencedate, int expressbookonpublicholidaydefaultid = 1)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CPBookingScheduleId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "frequency", frequency);

            if (nextoccurrencedate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "nextoccurrencedate", nextoccurrencedate);
            else
                this.AddFieldToBusinessDataObject(buisinessDataObject, "nextoccurrencedate", null);

            if (firstoccurrencedate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "firstoccurrencedate", firstoccurrencedate.Value);
            else
                this.AddFieldToBusinessDataObject(buisinessDataObject, "firstoccurrencedate", null);

            if (lastoccurrencedate.HasValue)
                this.AddFieldToBusinessDataObject(buisinessDataObject, "lastoccurrencedate", lastoccurrencedate);
            else
                this.AddFieldToBusinessDataObject(buisinessDataObject, "lastoccurrencedate", null);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "expressbookonpublicholidaydefaultid", expressbookonpublicholidaydefaultid);

            this.UpdateRecord(buisinessDataObject);
        }


        public void UpdateGenderPreference(Guid CPBookingScheduleId, int genderpreferenceid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CPBookingScheduleId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "genderpreferenceid", genderpreferenceid);

            this.UpdateRecord(buisinessDataObject);
        }

        public void DeleteCPBookingSchedule(Guid CPBookingScheduleId)
        {
            this.DeleteRecord(TableName, CPBookingScheduleId);
        }

        public List<Guid> GetByLocationId(Guid locationid)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "locationid", ConditionOperatorType.Equal, locationid);

            this.AddReturnField(query, TableName, PrimaryKeyName);

            query.Orders.Add(new OrderBy("createdon", SortOrder.Descending, TableName));

            return this.ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);

        }

    }
}
