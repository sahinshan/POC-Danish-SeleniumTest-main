using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class ProviderRoom : BaseClass
    {
        public string TableName = "ProviderRoom";
        public string PrimaryKeyName = "ProviderRoomId";

        public ProviderRoom()
        {
            AuthenticateUser();
        }

        public ProviderRoom(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public List<Guid> GetProviderRoomByName(string RoomName)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "RoomName", ConditionOperatorType.Equal, RoomName);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetProviderRoomByID(Guid ProviderRoomId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, ProviderRoomId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }



        public void UpdateProviderRoomMainPhone(Guid ProviderRoomID, string mainphone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ProviderRoom", "ProviderRoomId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ProviderRoomID", ProviderRoomID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "mainphone", mainphone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateProviderRoomOtherPhone(Guid ProviderRoomID, string otherphone)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ProviderRoom", "ProviderRoomId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ProviderRoomID", ProviderRoomID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "otherphone", otherphone);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateProviderRoomEmail(Guid ProviderRoomID, string Email)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ProviderRoom", "ProviderRoomId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ProviderRoomID", ProviderRoomID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "email", Email);

            this.UpdateRecord(buisinessDataObject);
        }

        public void UpdateStartDate(Guid ProviderRoomID, DateTime StartDate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject("ProviderRoom", "ProviderRoomId");

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ProviderRoomID", ProviderRoomID);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "startdate", StartDate);

            this.UpdateRecord(buisinessDataObject);
        }

        public Guid CreateProviderRoom(string Roomname, Guid ownerid, Guid providerid)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "Roomname", Roomname);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "providerid", providerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);


            return this.CreateRecord(buisinessDataObject);

        }

        public void DeleteProviderRoom(Guid ProviderRoomId)
        {
            this.DeleteRecord(TableName, ProviderRoomId);
        }

    }
}
