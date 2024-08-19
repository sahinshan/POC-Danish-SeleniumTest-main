using CareDirector.Sdk.Query;
using CareDirector.Sdk.SystemEntities;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace Phoenix.DBHelper.Models
{
    public class CPKeyworkerNotesAttachment : BaseClass
    {

        private string tableName = "cpkeyworkernotesattachment";
        private string primaryKeyName = "cpkeyworkernotesattachmentid";

        public CPKeyworkerNotesAttachment()
        {
            AuthenticateUser();
        }

        public CPKeyworkerNotesAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCPKeyworkerNotesAttachment(Guid ownerid, Guid personid, string title, DateTime date, Guid DocumentTypeId, Guid DocumentSubTypeId, Guid cppersonkeyworkernoteid, string FilePath, string caption)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DocumentTypeId", DocumentTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DocumentSubTypeId", DocumentSubTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "date", date);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "cppersonkeyworkernoteid", cppersonkeyworkernoteid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caption", caption);

            var PersonKeyworkerNoteAttachmentId = CreateRecord(buisinessDataObject);


            if (!string.IsNullOrEmpty(FilePath))
            {
                var fileId = UploadFile(FilePath, tableName, "fileid", PersonKeyworkerNoteAttachmentId);
                UpdateFileId(PersonKeyworkerNoteAttachmentId, fileId);
            }


            return PersonKeyworkerNoteAttachmentId;
        }

        public void UpdateFileId(Guid CPKeyworkerNotesAttachmentId, Guid? FileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CPKeyworkerNotesAttachmentId);

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

        public List<Guid> GetByPersonIDAndSortedByDate(Guid PersonId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "PersonId", ConditionOperatorType.Equal, PersonId);
            query.Orders.Add(new OrderBy("date", SortOrder.Descending, tableName));

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }        

        public List<Guid> GetByCpPersonKeyworkerNoteId(Guid CpPersonKeyworkerNoteId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cppersonkeyworkernoteid", ConditionOperatorType.Equal, CpPersonKeyworkerNoteId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByCpPersonKeyworkerNoteIdAndTitle(Guid CpPersonKeyworkerNoteId, string title)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cppersonkeyworkernoteid", ConditionOperatorType.Equal, CpPersonKeyworkerNoteId);
            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, title);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }




        public Dictionary<string, object> GetByID(Guid CPKeyworkerNotesAttachmentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CPKeyworkerNotesAttachmentId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCPKeyworkerNotesAttachment(Guid CPKeyworkerNotesAttachmentID)
        {
            this.DeleteRecord(tableName, CPKeyworkerNotesAttachmentID);
        }



    }
}
