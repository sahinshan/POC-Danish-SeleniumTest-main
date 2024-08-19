using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CpMobilityAttachment : BaseClass
    {

        private string tableName = "cpmobilityattachment";
        private string primaryKeyName = "cpmobilityattachmentid";

        public CpMobilityAttachment()
        {
            AuthenticateUser();
        }

        public CpMobilityAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCpMobilityAttachment(Guid ownerid, Guid personid, string title, DateTime date, Guid DocumentTypeId, Guid DocumentSubTypeId, Guid cppersonmobilityid, string FilePath, string caption)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "ownerid", ownerid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "personid", personid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "title", title);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DocumentTypeId", DocumentTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "DocumentSubTypeId", DocumentSubTypeId);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "date", date);

            this.AddFieldToBusinessDataObject(buisinessDataObject, "cppersonmobilityid", cppersonmobilityid);
            this.AddFieldToBusinessDataObject(buisinessDataObject, "caption", caption);

            var MobilityAttachmentId = CreateRecord(buisinessDataObject);


            if (!string.IsNullOrEmpty(FilePath))
            {
                var fileId = UploadFile(FilePath, tableName, "fileid", MobilityAttachmentId);
                UpdateFileId(MobilityAttachmentId, fileId);
            }


            return MobilityAttachmentId;
        }

        public void UpdateFileId(Guid cpmobilityattachmentid, Guid? FileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, cpmobilityattachmentid);

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

        public List<Guid> GetByCpPersonMobilityId(Guid CpPersonMobilityId)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cppersonmobilityid", ConditionOperatorType.Equal, CpPersonMobilityId);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }

        public List<Guid> GetByCpPersonKeyworkerNoteIdAndTitle(Guid CpPersonMobilityId, string title)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "cppersonmobilityid", ConditionOperatorType.Equal, CpPersonMobilityId);
            this.BaseClassAddTableCondition(query, "title", ConditionOperatorType.Equal, title);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }




        public Dictionary<string, object> GetByID(Guid CpMobilityAttachmentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CpMobilityAttachmentId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCpMobilityAttachment(Guid CpMobilityAttachmentID)
        {
            this.DeleteRecord(tableName, CpMobilityAttachmentID);
        }



    }
}
