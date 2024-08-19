using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class PersonAttachment : BaseClass
    {

        private string tableName = "PersonAttachment";
        private string primaryKeyName = "PersonAttachmentId";

        public PersonAttachment()
        {
            AuthenticateUser();
        }

        public PersonAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public void CreateMultipleAttachmentRecords(int TotalRecordsToCreate, Guid ownerid, Guid personid, string title, DateTime date, Guid DocumentTypeId, Guid DocumentSubTypeId, string FilePath)
        {
            var allRecordsToCreate = new List<BusinessData>();

            //we cannot insert more than 1000 records at once
            if (TotalRecordsToCreate > 100)
                TotalRecordsToCreate = 100;

            for (int i = 0; i < TotalRecordsToCreate; i++)
            {
                var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

                this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "DocumentTypeId", DocumentTypeId);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "DocumentSubTypeId", DocumentSubTypeId);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "date", date);

                this.AddFieldToBusinessDataObject(buisinessDataObject, "iscloned", 0);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
                this.AddFieldToBusinessDataObject(buisinessDataObject, "declared", 0);

                allRecordsToCreate.Add(buisinessDataObject);
            }

            var personAttachmentIds = this.CreateMultipleRecords(allRecordsToCreate);

            if (!string.IsNullOrEmpty(FilePath))
            {
                foreach (var personAttachmentId in personAttachmentIds)
                {
                    var fileId = UploadFile(FilePath, tableName, primaryKeyName, personAttachmentId);
                    UpdateFileId(personAttachmentId, fileId);
                }
            }
        }


        public Guid CreatePersonAttachment(Guid ownerid, Guid personid, string title, DateTime date, Guid DocumentTypeId, Guid DocumentSubTypeId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DocumentTypeId", DocumentTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DocumentSubTypeId", DocumentSubTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "date", date);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "iscloned", 0);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "inactive", false);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "declared", 0);

            return this.CreateRecord(buisinessDataObject);
        }

        public void UpdateFileId(Guid PersonAttachmentId, Guid? FileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, PersonAttachmentId);

            AddFieldToBusinessDataObject(buisinessDataObject, "fileid", FileId);

            this.UpdateRecord(buisinessDataObject);
        }

        public List<Guid> GetByPersonID(Guid PersonId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByPersonIDAndTitle(Guid PersonId, string title)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);
            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, title);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }




        public Dictionary<string, object> GetByID(Guid PersonAttachmentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, PersonAttachmentId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeletePersonAttachment(Guid PersonAttachmentID)
        {
            this.DeleteRecord(tableName, PersonAttachmentID);
        }



    }
}
