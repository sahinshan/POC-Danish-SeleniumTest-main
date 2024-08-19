using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ClinicRoom : BaseClass
    {
        public string TableName = "ClinicRoom";
        public string PrimaryKeyName = "ClinicRoomId";

        public ClinicRoom()
        {
            AuthenticateUser();
        }

        public ClinicRoom(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetClinicRoomByTitle(string title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public List<Guid> GetClinicRoomByID(Guid providerroomid)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "providerroomid", ConditionOperatorType.Equal, providerroomid);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }



        public void UpdateClinicRoomMainPhone(Guid ClinicRoomID, string mainphone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicRoom", "ClinicRoomId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicRoomID", ClinicRoomID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "mainphone", mainphone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateClinicRoomOtherPhone(Guid ClinicRoomID, string otherphone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicRoom", "ClinicRoomId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicRoomID", ClinicRoomID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "otherphone", otherphone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateClinicRoomEmail(Guid ClinicRoomID, string Email)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicRoom", "ClinicRoomId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicRoomID", ClinicRoomID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "email", Email);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStartDate(Guid ClinicRoomID, DateTime StartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ClinicRoom", "ClinicRoomId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ClinicRoomID", ClinicRoomID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", StartDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public Guid CreateClinicRoom(Guid ownerid, Guid OwningBusinessUnitId, Guid communityclinicdiaryviewsetupid, Guid providerroomid, Guid recurrencepatternid, DateTime startdate, TimeSpan starttime,
                                    TimeSpan endtime, Guid? enddate = null)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "communityclinicdiaryviewsetupid", communityclinicdiaryviewsetupid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "providerroomid", providerroomid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "recurrencepatternid", recurrencepatternid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "OwningBusinessUnitId", OwningBusinessUnitId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "starttime", starttime);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "endtime", endtime);


            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteClinicRoom(Guid ClinicRoomId)
        {
            this.DeleteRecord(TableName, ClinicRoomId);
        }

    }
}
