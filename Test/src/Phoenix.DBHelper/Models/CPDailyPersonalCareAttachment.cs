using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CPDailyPersonalCareAttachment : BaseClass
    {

        private string tableName = "cpdailypersonalcareattachment";
        private string primaryKeyName = "cpdailypersonalcareattachmentId";

        public CPDailyPersonalCareAttachment()
        {
            AuthenticateUser();
        }

        public CPDailyPersonalCareAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public void UpdateFileId(Guid CPDailyPersonalCareAttachmentId, Guid? FileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CPDailyPersonalCareAttachmentId);

            AddFieldToBusinessDataObject(buisinessDataObject, "fileid", FileId);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonDailyPersonalCareId(Guid cppersonpersonalcaredailyrecordid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cppersonpersonalcaredailyrecordid", ConditionOperatorType.Equal, cppersonpersonalcaredailyrecordid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid CPDailyPersonalCareAttachmentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPDailyPersonalCareAttachmentId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCPDailyPersonalCareAttachment(Guid CPDailyPersonalCareAttachmentID)
        {
            this.DeleteRecord(tableName, CPDailyPersonalCareAttachmentID);
        }

    }
}
