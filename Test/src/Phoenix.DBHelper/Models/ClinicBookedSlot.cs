using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ClinicBookedSlot : BaseClass
    {
        public string TableName = "ClinicBookedSlot";
        public string PrimaryKeyName = "ClinicBookedSlotId";

        public ClinicBookedSlot()
        {
            AuthenticateUser();
        }

        public ClinicBookedSlot(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetClinicBookedSlotByTitle(string title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public List<Guid> GetByHealthAppointmentID(Guid HealthAppointmentid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "HealthAppointmentid", ConditionOperatorType.Contains, HealthAppointmentid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public void UpdateClinicBookedSlotMainPhone(Guid ClinicBookedSlotID, string mainphone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicBookedSlot", "ClinicBookedSlotId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicBookedSlotID", ClinicBookedSlotID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "mainphone", mainphone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateClinicBookedSlotOtherPhone(Guid ClinicBookedSlotID, string otherphone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicBookedSlot", "ClinicBookedSlotId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicBookedSlotID", ClinicBookedSlotID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "otherphone", otherphone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateClinicBookedSlotEmail(Guid ClinicBookedSlotID, string Email)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicBookedSlot", "ClinicBookedSlotId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicBookedSlotID", ClinicBookedSlotID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "email", Email);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStartDate(Guid ClinicBookedSlotID, DateTime StartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicBookedSlot", "ClinicBookedSlotId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicBookedSlotID", ClinicBookedSlotID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", StartDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public Guid CreateClinicBookedSlot(Guid ownerid, Guid OwningBusinessUnitId, Guid communityclinicdiaryviewsetupid, Guid providerroomid, Guid recurrencepatternid, DateTime startdate, TimeSpan starttime,
                                    TimeSpan endtime)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "communityclinicdiaryviewsetupid", communityclinicdiaryviewsetupid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "providerroomid", providerroomid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "recurrencepatternid", recurrencepatternid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);


            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteClinicBookedSlot(Guid ClinicBookedSlotId)
        {
            this.DeleteRecord(TableName, ClinicBookedSlotId);
        }

    }
}
