using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class EmailAttachment : BaseClass
    {

        public string TableName = "EmailAttachment";
        public string PrimaryKeyName = "EmailAttachmentId";


        public EmailAttachment()
        {
            AuthenticateUser();
        }

        public EmailAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateEmailAttachment(Guid ownerid, bool inactive, Guid EmailId, string FilePath, string FileName)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", inactive);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "emailid", EmailId);

            var EmailAttachmentId = CreateRecord(buisinessDataObject);

            if (!string.IsNullOrEmpty(FilePath))
            {
                var fileId = UploadFile(FilePath, TableName, PrimaryKeyName, EmailAttachmentId);

                UpdateFileId(EmailAttachmentId, fileId);
            }

            return EmailAttachmentId;
        }

        public void UpdateFileId(Guid EmailAttachmentId, Guid? FileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(TableName, PrimaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, PrimaryKeyName, EmailAttachmentId);

            AddFieldToBusinessDataObject(buisinessDataObject, "fileid", FileId);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetEmailAttachmentByEmailID(Guid EmailId)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnField(query, TableName, PrimaryKeyName);

            this.BaseClassAddTableCondition(query, "EmailId", ConditionOperatorType.Equal, EmailId);

            return ExecuteDataQueryAndExtractGuidFields(query, PrimaryKeyName);
        }

        public Dictionary<string, object> GetEmailAttachmentByID(Guid EmailAttachmentId, params string[] FieldsToReturn)
        {
            var query = this.GetDataQueryObject(TableName, false, PrimaryKeyName);
            this.AddReturnFields(query, TableName, FieldsToReturn);

            this.BaseClassAddTableCondition(query, PrimaryKeyName, ConditionOperatorType.Equal, EmailAttachmentId);

            return ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteEmailAttachment(Guid EmailAttachmentId)
        {
            this.DeleteRecord(TableName, EmailAttachmentId);
        }
    }
}
