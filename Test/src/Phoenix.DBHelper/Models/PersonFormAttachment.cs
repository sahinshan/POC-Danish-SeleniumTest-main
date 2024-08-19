using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonFormAttachment : BaseClass
    {

        private string tableName = "PersonFormAttachment";
        private string primaryKeyName = "PersonFormAttachmentId";

        public PersonFormAttachment()
        {
            AuthenticateUser();
        }

        public PersonFormAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreatePersonFormAttachment(Guid ownerid, Guid personid, Guid personformid, string title, DateTime date, Guid DocumentTypeId, Guid DocumentSubTypeId, string FilePath, string FileName, Guid? DataRestrictionId = null)
        {

            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "personformid", personformid);
            AddFieldToBusinessDataObject(dataObject, "title", title);
            AddFieldToBusinessDataObject(dataObject, "date", date);
            AddFieldToBusinessDataObject(dataObject, "DocumentTypeId", DocumentTypeId);
            AddFieldToBusinessDataObject(dataObject, "DocumentSubTypeId", DocumentSubTypeId);
            AddFieldToBusinessDataObject(dataObject, "DataRestrictionId", DataRestrictionId);

            AddFieldToBusinessDataObject(dataObject, "iscloned", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "declared", 0);

            var CreatePersonFormAttachmentId = CreateRecord(dataObject);


            if (!string.IsNullOrEmpty(FilePath))
            {
                var fileId = UploadFile(FilePath, tableName, primaryKeyName, CreatePersonFormAttachmentId);

                UpdateFileId(CreatePersonFormAttachmentId, fileId);
            }

            return CreatePersonFormAttachmentId;

        }

        public void UpdateFileId(Guid CreatePersonFormAttachmentId, Guid? FileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CreatePersonFormAttachmentId);

            AddFieldToBusinessDataObject(buisinessDataObject, "fileid", FileId);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonFormFormID(Guid caseformid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonFormAttachment", ConditionOperatorType.Equal, caseformid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public Dictionary<string, object> GetByID(Guid PersonFormAttachmentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, PersonFormAttachmentId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonFormAttachmentRecord(Guid PersonFormAttachmentID)
        {
            this.DeleteRecord(tableName, PersonFormAttachmentID);
        }

    }
}
