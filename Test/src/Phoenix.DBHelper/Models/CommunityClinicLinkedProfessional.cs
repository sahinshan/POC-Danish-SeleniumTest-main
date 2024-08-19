using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CommunityClinicLinkedProfessional : BaseClass
    {
        public string TableName = "CommunityClinicLinkedProfessional";
        public string PrimaryKeyName = "CommunityClinicLinkedProfessionalId";

        public CommunityClinicLinkedProfessional()
        {
            AuthenticateUser();
        }

        public CommunityClinicLinkedProfessional(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCommunityClinicLinkedProfessional(Guid ownerid, Guid CommunityClinicDiaryViewSetupId, Guid SystemUserId, DateTime startdate, TimeSpan StartTime,
                                        TimeSpan EndTime, Guid RecurrencePatternId, string title)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "CommunityClinicDiaryViewSetupId", CommunityClinicDiaryViewSetupId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SystemUserId", SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartTime", StartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "EndTime", EndTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "RecurrencePatternId", RecurrencePatternId);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);

            return CreateRecord(buisinessDataObject);
        }

        public Guid CreateCommunityClinicLinkedProfessional(Guid ownerid, Guid CommunityClinicDiaryViewSetupId, Guid SystemUserId,
            DateTime startdate, DateTime? enddate, TimeSpan StartTime, TimeSpan EndTime,
            Guid RecurrencePatternId, string title)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "CommunityClinicDiaryViewSetupId", CommunityClinicDiaryViewSetupId);
            AddFieldToBusinessDataObject(buisinessDataObject, "SystemUserId", SystemUserId);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartTime", StartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "EndTime", EndTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "RecurrencePatternId", RecurrencePatternId);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);

            return CreateRecord(buisinessDataObject);
        }


        public void UpdateCommunityClinicLinkedProfessionalEnddate(Guid CommunityClinicLinkedProfessionalId, DateTime enddate)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, CommunityClinicLinkedProfessionalId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "enddate", enddate);

            this.UpdateRecord(buisinessDataObject);
        }
        public List<Guid> GetByTitle(string Title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", ConditionOperatorType.Equal, Title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CommunityClinicLinkedProfessionalId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CommunityClinicLinkedProfessionalId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }
        public List<Guid> GetLinkedProfessionalByID(Guid CommunityClinicDiaryViewSetupId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CommunityClinicDiaryViewSetupId", ConditionOperatorType.Equal, CommunityClinicDiaryViewSetupId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteCommunityClinicLinkedProfessional(Guid CommunityClinicLinkedProfessionalId)
        {
            this.DeleteRecord(TableName, CommunityClinicLinkedProfessionalId);
        }

    }
}
