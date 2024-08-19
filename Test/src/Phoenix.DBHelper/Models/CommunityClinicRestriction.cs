using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CommunityClinicRestriction : BaseClass
    {
        public string TableName = "CommunityClinicRestriction";
        public string PrimaryKeyName = "CommunityClinicRestrictionId";

        public CommunityClinicRestriction()
        {
            AuthenticateUser();
        }

        public CommunityClinicRestriction(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCommunityClinicRestriction(Guid CommunityClinicDiaryViewSetupId, Guid ownerid, int CommunityClinicRestrictionTypeId, Guid RecurrencePatternId,
            DateTime startdate, TimeSpan StartTime, TimeSpan EndTime, Guid ClinicRoomId, Guid HealthProfessionalId, string Details)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "CommunityClinicDiaryViewSetupId", CommunityClinicDiaryViewSetupId);
            AddFieldToBusinessDataObject(buisinessDataObject, "CommunityClinicRestrictionTypeId", CommunityClinicRestrictionTypeId);
            AddFieldToBusinessDataObject(buisinessDataObject, "RecurrencePatternId", RecurrencePatternId);
            AddFieldToBusinessDataObject(buisinessDataObject, "startdate", startdate);
            AddFieldToBusinessDataObject(buisinessDataObject, "StartTime", StartTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "EndTime", EndTime);
            AddFieldToBusinessDataObject(buisinessDataObject, "ClinicRoomId", ClinicRoomId);
            AddFieldToBusinessDataObject(buisinessDataObject, "HealthProfessionalId", HealthProfessionalId);

            AddFieldToBusinessDataObject(buisinessDataObject, "Details", Details);

            return CreateRecord(buisinessDataObject);
        }

        public List<Guid> GetByTitle(string Title)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "Title", ConditionOperatorType.Equal, Title);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CommunityClinicRestrictionId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, CommunityClinicRestrictionId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetByDiaryViewSetupId(Guid CommunityClinicDiaryViewSetupId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "CommunityClinicDiaryViewSetupId", ConditionOperatorType.Equal, CommunityClinicDiaryViewSetupId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public void DeleteCommunityClinicRestriction(Guid CommunityClinicRestrictionId)
        {
            this.DeleteRecord(TableName, CommunityClinicRestrictionId);
        }

    }
}
