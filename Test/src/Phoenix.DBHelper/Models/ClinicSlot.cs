using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ClinicSlot : BaseClass
    {
        public string TableName = "ClinicSlot";
        public string PrimaryKeyName = "ClinicSlotId";

        public ClinicSlot()
        {
            AuthenticateUser();
        }

        public ClinicSlot(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetClinicSlotByTitle(string title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public List<Guid> GetClinicSlotByID(Guid providerRoomid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerRoomid", ConditionOperatorType.Contains, providerRoomid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public void UpdateClinicSlotMainPhone(Guid ClinicSlotID, string mainphone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicSlot", "ClinicSlotId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicSlotID", ClinicSlotID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "mainphone", mainphone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateClinicSlotOtherPhone(Guid ClinicSlotID, string otherphone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicSlot", "ClinicSlotId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicSlotID", ClinicSlotID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "otherphone", otherphone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateClinicSlotEmail(Guid ClinicSlotID, string Email)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicSlot", "ClinicSlotId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicSlotID", ClinicSlotID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "email", Email);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStartDate(Guid ClinicSlotID, DateTime StartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicSlot", "ClinicSlotId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicSlotID", ClinicSlotID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", StartDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public Guid CreateClinicSlot(Guid ownerid, Guid OwningBusinessUnitId, Guid communityclinicdiaryviewsetupid, Guid providerroomid, Guid recurrencepatternid, DateTime startdate, TimeSpan starttime,
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

        public void DeleteClinicSlot(Guid ClinicSlotId)
        {
            this.DeleteRecord(TableName, ClinicSlotId);
        }

    }
}
