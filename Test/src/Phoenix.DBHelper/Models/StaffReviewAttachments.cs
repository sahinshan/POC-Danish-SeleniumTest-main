using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class StaffReviewAttachment : BaseClass
    {
        public string TableName { get { return "StaffReviewAttachment"; } }
        public string PrimaryKeyName { get { return "StaffReviewAttachmentid"; } }

        public StaffReviewAttachment()
        {
            AuthenticateUser();
        }

        public StaffReviewAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateStaffReviewAttachment(Guid ownerid, Guid staffreviewid, string title, DateTime date, Guid documenttypeid, Guid documentsubtypeid, string FilePath, string FileExtension, string FileName)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(buisinessDataObject, "staffreviewid", staffreviewid);
            AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            AddFieldToBusinessDataObject(buisinessDataObject, "date", date);
            AddFieldToBusinessDataObject(buisinessDataObject, "documenttypeid", documenttypeid);
            AddFieldToBusinessDataObject(buisinessDataObject, "documentsubtypeid", documentsubtypeid);

            var staffReviewAttachmentId = CreateRecord(buisinessDataObject);


            if (!string.IsNullOrEmpty(FilePath))
            {

                var fileId = UploadFile(FilePath, TableName, "fileid", staffReviewAttachmentId);

                UpdateFileId(staffReviewAttachmentId, fileId);
            }

            return staffReviewAttachmentId;
        }

        public void UpdateFileId(Guid PersonAttachmentId, Guid? FileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, PersonAttachmentId);

            AddFieldToBusinessDataObject(buisinessDataObject, "fileid", FileId);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetStaffReviewAttachmentByTitle(string Title, Guid staffreviewid)
        {
            DataQuery query = this.GetDataQueryObject("StaffReviewAttachment", false, "StaffReviewAttachmentId");

            this.BaseClassAddTableCondition(query, "Title", ConditionOperatorType.Equal, Title);
            this.BaseClassAddTableCondition(query, "staffreviewid", ConditionOperatorType.Equal, staffreviewid);

            this.AddReturnField(query, "StaffReviewAttachment", "StaffReviewAttachmentid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "StaffReviewAttachmentid");
        }

        public List<Guid> GetByStaffReviewId(Guid staffreviewid)
        {
            DataQuery query = this.GetDataQueryObject("StaffReviewAttachment", false, "StaffReviewAttachmentId");

            this.BaseClassAddTableCondition(query, "staffreviewid", ConditionOperatorType.Equal, staffreviewid);

            this.AddReturnField(query, "StaffReviewAttachment", "StaffReviewAttachmentid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "StaffReviewAttachmentid");
        }

        public Dictionary<string, object> GetByID(Guid StaffReviewAttachmentid, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "StaffReviewAttachmentid", ConditionOperatorType.Equal, StaffReviewAttachmentid);

            this.AddReturnFields(query, TableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public List<Guid> GetStaffReviewAttachmentByPersonID(Guid personid)
        {
            DataQuery query = this.GetDataQueryObject("StaffReviewAttachment", false, "StaffReviewAttachmentId");
            this.BaseClassAddTableCondition(query, "personid", ConditionOperatorType.Equal, personid);
            this.AddReturnField(query, "StaffReviewAttachment", "StaffReviewAttachmentid");

            return this.ExecuteDataQueryAndExtractGuidFields(query, "StaffReviewAttachmentid");
        }

        public void DeleteStaffReviewAttachment(Guid StaffReviewAttachmentID)
        {
            this.DeleteRecord(TableName, StaffReviewAttachmentID);
        }

    }
}
