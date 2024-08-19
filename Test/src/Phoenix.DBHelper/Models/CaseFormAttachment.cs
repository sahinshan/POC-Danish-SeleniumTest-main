using CareDirector.Sdk.Query;
using CareWorks.Foundation.Enums;
using System;
using System.Collections.Generic;

namespace Phoenix.DBHelper.Models
{
    public class CaseFormAttachment : BaseClass
    {

        private string tableName = "CaseFormAttachment";
        private string primaryKeyName = "CaseFormAttachmentId";

        public CaseFormAttachment()
        {
            AuthenticateUser();
        }

        public CaseFormAttachment(CareDirector.Sdk.ServiceResponse.AuthenticateResponse AuthenticationData)
        {
            SetServiceConnectionDataFromAuthenticationResponse(AuthenticationData);
        }

        public Guid CreateCaseFormAttachment(Guid ownerid, Guid personid, Guid caseid, Guid caseformid, string title, DateTime date, Guid DocumentTypeId, Guid DocumentSubTypeId, string FilePath, string FileName)
        {

            var dataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            AddFieldToBusinessDataObject(dataObject, "ownerid", ownerid);
            AddFieldToBusinessDataObject(dataObject, "personid", personid);
            AddFieldToBusinessDataObject(dataObject, "caseid", caseid);
            AddFieldToBusinessDataObject(dataObject, "caseformid", caseformid);
            AddFieldToBusinessDataObject(dataObject, "title", title);
            AddFieldToBusinessDataObject(dataObject, "date", date);
            AddFieldToBusinessDataObject(dataObject, "DocumentTypeId", DocumentTypeId);
            AddFieldToBusinessDataObject(dataObject, "DocumentSubTypeId", DocumentSubTypeId);

            AddFieldToBusinessDataObject(dataObject, "iscloned", 0);
            AddFieldToBusinessDataObject(dataObject, "inactive", false);
            AddFieldToBusinessDataObject(dataObject, "declared", 0);


            var CreateCaseFormAttachmentId = CreateRecord(dataObject);


            if (!string.IsNullOrEmpty(FilePath))
            {
                var fileId = UploadFile(FilePath, tableName, primaryKeyName, CreateCaseFormAttachmentId);

                UpdateFileId(CreateCaseFormAttachmentId, fileId);
            }

            return CreateCaseFormAttachmentId;

        }


        public void UpdateFileId(Guid CaseFormAttachmentId, Guid? FileId)
        {
            var buisinessDataObject = GetBusinessDataBaseObject(tableName, primaryKeyName);

            this.AddFieldToBusinessDataObject(buisinessDataObject, primaryKeyName, CaseFormAttachmentId);

            AddFieldToBusinessDataObject(buisinessDataObject, "fileid", FileId);

            this.UpdateRecord(buisinessDataObject);
        }


        public List<Guid> GetByCaseFormID(Guid caseformid)
        {
            var query = this.GetDataQueryObject(tableName, false, primaryKeyName);
            this.AddReturnField(query, tableName, primaryKeyName);

            this.BaseClassAddTableCondition(query, "caseformid", ConditionOperatorType.Equal, caseformid);

            return ExecuteDataQueryAndExtractGuidFields(query, primaryKeyName);
        }




        public Dictionary<string, object> GetByID(Guid CaseFormAttachmentId, params string[] Fields)
        {
            DataQuery query = this.GetDataQueryObject(tableName, false, primaryKeyName);

            this.BaseClassAddTableCondition(query, primaryKeyName, ConditionOperatorType.Equal, CaseFormAttachmentId);

            this.AddReturnFields(query, tableName, Fields);

            return this.ExecuteDataQueryAndExtractFirstResultFields(query);
        }

        public void DeleteCaseFormAttachment(Guid CaseFormAttachmentID)
        {
            this.DeleteRecord(tableName, CaseFormAttachmentID);
        }



    }
}
